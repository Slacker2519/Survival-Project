using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    public GameData Data => _data;
    [SerializeField] private GameData _data;

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
