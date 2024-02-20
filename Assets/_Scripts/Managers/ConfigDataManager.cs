using UnityEngine;

public class ConfigDataManager : SingletonMono<ConfigDataManager>
{
    // Config data is the base data when you start game fresh new
    public DataAssets DataAssets => _dataAssets;
    [SerializeField] private DataAssets _dataAssets;

    public void LoadGameConfigData()
    {
        // Assign config data from _dataAssets to config data list from class ConfigDataList
    }
}
