using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // New input system ref
    public GameInput Input => _gameInput;
    [SerializeField] private GameInput _gameInput;

    public BaseBody Player => _player;
    private BaseBody _player;

    public List<BaseEnemy> EnemiesList => _enemiesList;
    private List<BaseEnemy> _enemiesList;

    private void Start()
    {
        _enemiesList = new List<BaseEnemy>();

        SpawnPlayer();
        SpawnEnemy();
    }

    private void SpawnPlayer()
    {
        var characterPicked = GameManager.Instance.Data.CharacterPicked.Name;
        var character = PoolManager.Instance.SpawnCharacter(characterPicked);
        if (character.TryGetComponent<BaseBody>(out BaseBody player))
        {
            _player = player;
            var configManager = ConfigDataManager.Instance;
            CharacterConfigData stat = configManager.DataAssets.GetCharacterConfig(characterPicked);
            _player.InitCharacterStats(stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed);
            //GameManager.Instance.SaveGameData();
        }
    }

    private void SpawnEnemy()
    {
        EnemyConfigData firstEnemy = ConfigDataManager.Instance.DataAssets.GetEnemyConfig(EnemyEnum.Enemy1);
        GameObject enemyObj = PoolManager.Instance.SpawnEnemy(firstEnemy.Name);
        enemyObj.transform.position = GetEnemySpawnedPos();
        if (enemyObj.TryGetComponent<BaseEnemy>(out BaseEnemy enemy))
        {
            _enemiesList.Add(enemy);
            EnemyConfigData stat = firstEnemy;
            enemy.InitCharacterStats(stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed);
            //GameManager.Instance.SaveGameData();
        }
    }

    private Vector3 GetEnemySpawnedPos()
    {
        float xCoordinate;
        float yCoordinate;

        int leftOrRight = Mathf.RoundToInt(Random.Range(0, 2));
        xCoordinate = leftOrRight < 1 ? Random.Range(-.5f, -.1f) : Random.Range(1.1f, 1.5f);

        int topOrBottom = Mathf.RoundToInt(Random.Range(0, 2));
        yCoordinate = topOrBottom < 1 ? Random.Range(1.1f, 1.5f) : Random.Range(-.5f, -.1f);
        
        Vector3 newPos = Camera.main.ViewportToWorldPoint(new Vector3(xCoordinate, yCoordinate, 0));
        return new Vector3(newPos.x, newPos.y, 0);
    }
}
