using Elite.GangGang.Utils;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : SingletonMono<LevelController>
{
    #region Properties
    [SerializeField] private double _currentTime = 0;
    [SerializeField] private List<BaseEnemy> _enemyList;

    private BaseCharacter _player;
    private LevelDataSO _levelDataSO;
    private WaveData _currentPhase;

    private int _maxEnemyNumber;
    private int _currentWave = 0;
    private float _enemySpawnTime;
    private float _currentEnemySpawnTime = 0;
    private int _numberSpawnEnemy;
    #endregion

    #region Get Set
    public BaseCharacter Player => _player;
    public List<BaseEnemy> EnemyList => _enemyList;
    public int CurrentWave => _currentWave;
    public float EnemySpawnTime => _enemySpawnTime;
    public int NumberSpawnEnemy => _numberSpawnEnemy;
    #endregion

    private void Awake()
    {
        _enemyList = new List<BaseEnemy>();
    }

    private void Start()
    {
        this.RegisterEvent(EventID.OnEnemyDie, GetNextWave);
        this.RegisterEvent(EventID.OnFinishLevel, CompleteThisLevel);

        _currentWave = 0;
        _levelDataSO = GameUtilities.LoadClassicLevelData(0);
        Debug.Log(_levelDataSO.name);
        _currentPhase = _levelDataSO.PhaseDatas[_currentWave];
        _currentTime = _levelDataSO.LevelTime;
        _currentEnemySpawnTime = 0;

        _numberSpawnEnemy = _currentPhase.SpawnAmount;
        _enemySpawnTime = _currentPhase.SpawnInterval;
        _maxEnemyNumber = _currentPhase.MaxEnemy;

        SpawnPlayer();
        SpawnEnemyByWave(_currentPhase.WaveEnemyData);
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        _currentEnemySpawnTime += Time.deltaTime;

        CalculateEnemyWave();

        if (UnityEngine.Input.GetKeyDown(KeyCode.V))
        {
            foreach (var item in _enemyList)
            {
                item.ChangeState(EnemyStateEnum.Attacking);
            }
        }
    }

    private void CalculateEnemyWave()
    {
        if (_currentEnemySpawnTime >= _enemySpawnTime)
        {
            _currentEnemySpawnTime = 0;
            SpawnEnemyByWave(_currentPhase.WaveEnemyData);
        }
    }

    private void GetNextWave(object obj = null)
    {
        if (_currentTime > 0 && _enemyList.Count <= 0)
        {
            if (_currentWave < _levelDataSO.PhaseDatas.Count - 1) { _currentWave++; }

            _currentPhase = _levelDataSO.PhaseDatas[_currentWave];
            _numberSpawnEnemy = _currentPhase.SpawnAmount;
            _enemySpawnTime = _currentPhase.SpawnInterval;
            _maxEnemyNumber = _currentPhase.MaxEnemy;
            SpawnEnemyByWave(_currentPhase.WaveEnemyData);
        }
    }

    private void CompleteThisLevel(object obj = null)
    {
        this.UnregisterEvent(EventID.OnEnemyDie, GetNextWave);
        this.UnregisterEvent(EventID.OnFinishLevel, CompleteThisLevel);
    }

    private void SpawnPlayer()
    {
        var characterPicked = DataManager.Instance.Data.CharacterPicked.Name;
        var character = PoolManager.Instance.SpawnCharacter(characterPicked);

        if (character.TryGetComponent<BaseCharacter>(out BaseCharacter player))
        {
            _player = player;
            var configManager = DataManager.Instance;
            CharacterConfigData stat = configManager.DataAssets.GetCharacterConfig(characterPicked);

            _player.InitCharacterStats(stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed,
                stat.CritRate, stat.CritDamage, stat.AttackSpeed, stat.LevelExpCap, stat.PickupRange);

            HealthBar healthBar = FindObjectOfType<HealthBar>();

            if (healthBar)
            {
                healthBar.Setup(character.GetComponent<BaseCharacter>());
            }
        }
    }

    void SpawnEnemyByWave(List<EnemyWaveData> listEnemyWave)
    {
        if (_enemyList.Count < _maxEnemyNumber)
        {
            List<EnemyWaveData> tempListEnemyWave = listEnemyWave;
            int numRemain = _numberSpawnEnemy;
            for (int i = 0; i < tempListEnemyWave.Count; i++)
            {
                if (numRemain <= 0) break;

                int numSpawn = Random.Range(1, numRemain);
                EnemyWaveData data = tempListEnemyWave[i];

                if (SpawnEnemy(data.number, numSpawn, data.rank))
                    numRemain -= numSpawn;
            }
        }
    }

    private bool SpawnEnemy(int maxEnemyNumber, int numSpawn, EnemyRank rank, EnemyEnum enemyEnum = EnemyEnum.Laucent)
    {
        if (numSpawn <= 0) return false;

        int numEnumRemain = _enemyList.Count(enemy => enemy.EnemyStat.Rank == rank);

        if (numEnumRemain < maxEnemyNumber)
        {
            var configManager = DataManager.Instance;
            EnemyConfigData stat = configManager.DataAssets.GetEnemyConfigByRank(rank);

            for (int i = 0; i < numSpawn; i++)
            {
                SpawnEnemy(stat);
            }

            return true;
        }

        return false;
    }

    void SpawnEnemy(EnemyConfigData stat)
    {
        GameObject enemyObj = PoolManager.Instance.SpawnEnemy(stat.Name);
        enemyObj.transform.position = GetEnemySpawnedPos();
        BaseEnemy enemy = enemyObj.GetComponent<BaseEnemy>();
        enemy.InitEnemyStat(stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed, stat.Rank);
        _enemyList.Add(enemy);
    }

    public Vector3 GetEnemySpawnedPos()
    {
        float xCoordinate;
        float yCoordinate;
        Vector3 newPos;

        int leftOrRight = Mathf.RoundToInt(Random.Range(0f, 2f));
        xCoordinate = leftOrRight < 1 ? Random.Range(-.5f, -.1f) : Random.Range(1.1f, 1.5f);
        float secondXCoordinate = Random.Range(0f, 1f);

        int topOrBottom = Mathf.RoundToInt(Random.Range(0f, 2f));
        yCoordinate = topOrBottom < 1 ? Random.Range(1.1f, 1.5f) : Random.Range(-.5f, -.1f);
        float secondYCoordinate = Random.Range(0f, 1f);

        switch (Mathf.RoundToInt(Random.Range(0, 3)))
        {
            case 0:
                newPos = Camera.main.ViewportToWorldPoint(new Vector3(xCoordinate, yCoordinate, 0));
                break;
            case 1:
                newPos = Camera.main.ViewportToWorldPoint(new Vector3(secondXCoordinate, yCoordinate, 0));
                break;
            case 2:
                newPos = Camera.main.ViewportToWorldPoint(new Vector3(xCoordinate, secondYCoordinate, 0));
                break;
            case 3:
                newPos = Camera.main.ViewportToWorldPoint(new Vector3(secondXCoordinate, secondYCoordinate, 0));
                break;
            default:
                newPos = Camera.main.ViewportToWorldPoint(new Vector3(secondXCoordinate, secondYCoordinate, 0));
                break;
        }
        return new Vector3(newPos.x, newPos.y, 0);
    }
}
