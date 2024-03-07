using System.IO;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    // Config data is the base data when you start game fresh new
    public DataAssets DataAssets => _dataAssets;
    [SerializeField] private DataAssets _dataAssets;

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
            _data = new GameData(_dataAssets);
        }
        else
        {
            _data = JsonUtility.FromJson<GameData>(File.ReadAllText(filePath));
        }

        return true;
    }
}
