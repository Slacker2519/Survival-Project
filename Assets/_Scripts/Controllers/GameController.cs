using Elite.GangGang.Utils;
using Gameplay;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // New input system ref
    public GameInput PlayerInput => _gameInput;
    [SerializeField] private GameInput _gameInput;

    public BaseCharacter Player => _player;
    private BaseCharacter _player;

    public List<BaseEnemy> EnemiesList => _enemiesList;
    [SerializeField] private List<BaseEnemy> _enemiesList;
    private int _maxEnemiesNumber;

    private double _currentTime;

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
        _currentTime = Constants.WaveDuration;
        _currentEnemySpawnTime = 0;
        _numberSpawnEnemy = GameUtilities.GetEnemySpawnNumber(_currentWave);
        _enemySpawnTime = GameUtilities.GetEnemySpawnDuration(_currentWave);
        _maxEnemiesNumber = GameUtilities.GetEnemyMaxNumber(_currentWave);
        SpawnPlayer();
        SpawnEnemy();
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
            _numberSpawnEnemy = GameUtilities.GetEnemySpawnNumber(_currentWave);
            _enemySpawnTime = GameUtilities.GetEnemySpawnDuration(_currentWave);
            _maxEnemiesNumber = GameUtilities.GetEnemyMaxNumber(_currentWave);
            SpawnEnemy();
        }

        if (_currentEnemySpawnTime >= _enemySpawnTime)
        {
            _currentEnemySpawnTime = 0;
            SpawnEnemy();
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

    private void SpawnEnemy()
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
                enemy.InitEnemyStat(EnemyEnum.Enemy1, stat.Health, stat.Defense, stat.Damage, stat.Speed);
                _enemiesList.Add(enemy);
            }
        }
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
        
        switch(Mathf.RoundToInt(Random.Range(0, 3)))
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
