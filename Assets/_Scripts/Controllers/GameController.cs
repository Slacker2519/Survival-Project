using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMono<GameController>
{
    // New input system ref
    public GameInput Input => _gameInput;
    [SerializeField] private GameInput _gameInput;

    public BaseCharacter Player => _player;
    private BaseCharacter _player;

    private void Start()
    {
        var character = PoolManager.Instance.SpawnCharacter(CharacterEnum.Character1);
        if (character.TryGetComponent<BaseCharacter>(out BaseCharacter player))
        {
            _player = player;
            var configManager = ConfigDataManager.Instance;
            CharacterConfigData stat = configManager.DataAssets.GetCharacterConfig(CharacterEnum.Character1);
            _player.InitCharacterStats(stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed);
        }
    }
}
