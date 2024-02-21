using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField] private GameController _gameControllerPrefab;

    public GameController Controller => _controller;
    private GameController _controller;

    public GameData Data => _data;
    [SerializeField] private GameData _data;

    private void Awake()
    {
        if (LoadGameData())
        {
            _controller = Instantiate(_gameControllerPrefab);
        }
    }

    public void SaveGameData()
    {
        string text = JsonUtility.ToJson(_data);
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");
        File.WriteAllText(filePath, text);
    }

    public bool LoadGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");
        if (!File.Exists(filePath))
        {
            _data = new GameData();
        }
        else
        {
            _data = JsonUtility.FromJson<GameData>(File.ReadAllText(filePath));
        }

        return true;
    }
}
