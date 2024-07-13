using Elite.GangGang.Utils;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : SingletonMono<LevelController>
{
    #region Properties
    [SerializeField] private int _level;
    [SerializeField] private double _currentTime = 0;
    [SerializeField] private List<BaseEnemy> _enemyList;

    private BaseCharacter _player;
    private LevelDataSO _levelDataSO;
    private LevelData _levelData;

    private int _maxEnemy;
    private int _currentWave = 0;
    private float _enemySpawnDuration;
    private float _currentEnemySpawnDuration = 0;
    private int _enemySpawnAmount;
    #endregion

    #region Get Set
    public BaseCharacter Player => _player;
    public List<BaseEnemy> EnemyList => _enemyList;
    public int CurrentWave => _currentWave;
    public float EnemySpawnTime => _enemySpawnDuration;
    public int EnemySpawnAmount => _enemySpawnAmount;
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
        _levelDataSO = GameUtilities.LoadClassicLevelData(_level);
        Debug.Log(_levelDataSO.name);
        _levelData = _levelDataSO.LevelDatasList[_currentWave];
        _currentTime = _levelDataSO.LevelTime;
        _currentEnemySpawnDuration = 0;

        _enemySpawnAmount = _levelData.SpawnAmount;
        _enemySpawnDuration = _levelData.SpawnInterval;
        _maxEnemy = _levelData.MaxEnemy;

        SpawnPlayer();
        SpawnEnemyByWave(_levelData.WaveEnemyData);
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        _currentEnemySpawnDuration += Time.deltaTime;

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
        if (_currentEnemySpawnDuration >= _enemySpawnDuration)
        {
            _currentEnemySpawnDuration = 0;
            SpawnEnemyByWave(_levelData.WaveEnemyData);
        }
    }

    private void GetNextWave(object obj = null)
    {
        if (_currentTime > 0 && _enemyList.Count <= 0)
        {
            if (_currentWave < _levelDataSO.LevelDatasList.Count - 1) { _currentWave++; }

            _levelData = _levelDataSO.LevelDatasList[_currentWave];
            _enemySpawnAmount = _levelData.SpawnAmount;
            _enemySpawnDuration = _levelData.SpawnInterval;
            _maxEnemy = _levelData.MaxEnemy;
            SpawnEnemyByWave(_levelData.WaveEnemyData);
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
        if (_enemyList.Count < _maxEnemy)
        {
            List<EnemyWaveData> tempListEnemyWave = listEnemyWave;
            int numRemain = _enemySpawnAmount;
            for (int i = 0; i < tempListEnemyWave.Count; i++)
            {
                if (numRemain <= 0) break;

                int numSpawn = 0;
                Mathf.Clamp(numSpawn, 1, numRemain);
                EnemyWaveData data = tempListEnemyWave[i];

                if (SpawnEnemy(data.number, numSpawn, data.name))
                    numRemain -= numSpawn;
            }
        }
    }

    private bool SpawnEnemy(int maxEnemyNumber, int numSpawn, EnemyEnum name)
    {
        if (numSpawn <= 0) return false;

        int numEnumRemain = _enemyList.Count(enemy => enemy.EnemyStat.Name == name);

        if (numEnumRemain < maxEnemyNumber)
        {
            var configManager = DataManager.Instance;
            EnemyConfigData stat = configManager.DataAssets.GetEnemyConfig(name);

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
