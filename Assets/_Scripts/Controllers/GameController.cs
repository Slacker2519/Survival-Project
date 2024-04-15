using Elite.GangGang.Utils;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // New input system ref
    public GameInput PlayerInput => _gameInput;
    [SerializeField] private GameInput _gameInput;

    public BaseCharacter Player => _player;
    private BaseCharacter _player;
    private LevelDataSO _levelDataSO;
    private WaveData _curPharse;
    public List<BaseEnemy> EnemiesList => _enemiesList;
    [SerializeField] private List<BaseEnemy> _enemiesList;
    private int _maxEnemiesNumber;

    [SerializeField] private double _currentTime;

    public int CurrentWave => _currentWave;
    private int _currentWave;

    public float EnemySpawnTime => _enemySpawnTime;
    private float _enemySpawnTime;
    private float _currentEnemySpawnTime;

    public int NumberSpawnEnemy => _numberSpawnEnemy;
    private int _numberSpawnEnemy;

    private void Awake()
    {
        _gameInput = GetComponent<GameInput>();
        _enemiesList = new List<BaseEnemy>();
    }

    private void Start()
    {
        _currentWave = 0;
        _levelDataSO = GameUtilities.LoadClassicLevelData(0);
        Debug.Log(_levelDataSO.name);
        _curPharse = _levelDataSO.pharseDatas[_currentWave];
        _currentTime = Constants.WaveDuration;
        _currentEnemySpawnTime = 0;

        _numberSpawnEnemy = _curPharse.SpawnAmount;  //GameUtilities.GetEnemySpawnNumber(_currentWave);
        _enemySpawnTime = _curPharse.SpawnInterval;  /*GameUtilities.GetEnemySpawnDuration(_currentWave);*/
        _maxEnemiesNumber = _curPharse.MaxEnemy;  /*GameUtilities.GetEnemyMaxNumber(_currentWave);*/
        SpawnPlayer();
        SpawnEnemyByWave(_curPharse.WaveEnemyData);
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        _currentEnemySpawnTime += Time.deltaTime;

        CalculateEnemyWave();
    }

    private void CalculateEnemyWave()
    {
        if (_currentTime <= 0)
        {
            _currentTime = Constants.WaveDuration;
            _currentWave++;
            _curPharse = _levelDataSO.pharseDatas[_currentWave];
            _numberSpawnEnemy = _curPharse.SpawnAmount;  //GameUtilities.GetEnemySpawnNumber(_currentWave);
            _enemySpawnTime = _curPharse.SpawnInterval;  /*GameUtilities.GetEnemySpawnDuration(_currentWave);*/
            _maxEnemiesNumber = _curPharse.MaxEnemy;  /*GameUtilities.GetEnemyMaxNumber(_currentWave);*/
            SpawnEnemyByWave(_curPharse.WaveEnemyData);
            //SpawnEnemy();
            //SpawnEnemy()
        }
        
        if (_currentEnemySpawnTime >= _enemySpawnTime)
        {
            _currentEnemySpawnTime = 0;
            SpawnEnemyByWave(_curPharse.WaveEnemyData);
        }
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
            //GameManager.Instance.SaveGameData();
        }
    }

   /* private void SpawnEnemy()
    {
        if (EnemiesList.Count < _maxEnemiesNumber)
        {
            var configManager = DataManager.Instance;
            EnemyConfigData stat = configManager.DataAssets.GetEnemyConfig(EnemyEnum.Enemy1);

            for (int i = 0; i < Mathf.RoundToInt(Mathf.Clamp(_numberSpawnEnemy, 0, _numberSpawnEnemy)); i++)
            {
                GameObject enemyObj = PoolManager.Instance.SpawnEnemy(EnemyEnum.Enemy1);
                enemyObj.transform.position = GetEnemySpawnedPos();
                Enemy1 enemy = enemyObj.GetComponent<Enemy1>();
                enemy.InitEnemyStat(EnemyEnum.Enemy1, stat.Health, stat.Defense, stat.Damage, stat.Speed,EnemyRank.Small);
                _enemiesList.Add(enemy);
            }
        }
    }*/

    void SpawnEnemyByWave(List<EnemyWaveData> _listEnemyWava)
    {
        if (EnemiesList.Count < _maxEnemiesNumber)
        {
            List<EnemyWaveData> listEnemyWav = _listEnemyWava;
            int numRemain = _numberSpawnEnemy;
            for(int i = 0; i < listEnemyWav.Count; i++)
            {
                if(numRemain<=0)
                {
                    break;
                }
                int numSpawn = Random.Range(1, numRemain);
                EnemyWaveData data = listEnemyWav[i];
                if(SpawnEnemy(data.number,numSpawn, data.rank))
                    numRemain -= numSpawn;
            }
            /*for (int i = 0; i < listEnemyWav.Count; i++)
            {
                EnemyWaveData data = listEnemyWav[i];
                //Random enemy here
                
                //SpawnEnemy(data.number, data.rank);
            }*/
        }
        
    }
    /*public int CalcuLateSwapNum(int _maxEnemiesNumber,int maxEnemy, EnemyRank rank)
    {
        int numEnemtRemain = EnemiesList.Count(enemy => enemy.EnemyStat.Rank == rank);
        if (numEnemtRemain < _maxEnemiesNumber)
        {
            
        }
    }*/
    private bool SpawnEnemy(int _maxEnemiesNumber,int numSpaw,EnemyRank rank, EnemyEnum enemyEnum = EnemyEnum.Enemy1)
    {
        if (numSpaw <= 0) return false;
        int numEnemtRemain = EnemiesList.Count(enemy => enemy.EnemyStat.Rank == rank);
        if (numEnemtRemain < _maxEnemiesNumber)
        {
            var configManager = DataManager.Instance;
            EnemyConfigData stat = configManager.DataAssets.GetEnemyConfigByRank(rank);

            /*for (int i = 0; i < Mathf.RoundToInt(Mathf.Clamp(_numberSpawnEnemy, 0, _numberSpawnEnemy)); i++)
            {
                SpawnEnemy(stat);
            }*/
            for (int i = 0; i < numSpaw; i++)
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
        enemy.InitEnemyStat(stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed,stat.Rank);
        _enemiesList.Add(enemy);
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
