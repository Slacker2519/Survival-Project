using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterConfigData
{
    public CharacterEnum Name;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;
    public GameObject Prefab;
}

[Serializable]
public class EnemyConfigData
{
    public EnemyEnum Name;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "DataAssets", menuName = "ScripableObjects/DataAssets", order = 0)]
public class DataAssets : ScriptableObject
{
    [Header("===== Characters Config Data =====")]
    [SerializeField] 
    private List<CharacterConfigData> _configCharactersStatList;
    public List<CharacterConfigData> ConfigCharactersStatList => _configCharactersStatList;

    [Space (30)]

    [Header("===== Enemies Config Data =====")]
    [SerializeField]
    private List<EnemyConfigData> _configEnemiesStatList;
    public List<EnemyConfigData> ConfigEnemiesStatList => _configEnemiesStatList;

    public CharacterConfigData GetCharacterConfig(CharacterEnum characterName)
    {
        return _configCharactersStatList.Find(x => x.Name == characterName);
    }

    public EnemyConfigData GetEnemyConfig(EnemyEnum enemyName)
    {
        return _configEnemiesStatList.Find(x => x.Name == enemyName);
    }
}
