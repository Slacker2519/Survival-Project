using System;
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
    public int CritRate;
    public long CritDamage;
    public long AttackSpeed;
    public long LevelExpCap;
    public float PickupRange;
    public GameObject Prefab;
}

[Serializable]
public class EnemyConfigData
{
    public EnemyEnum Name;
    public EnemyRank Rank;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;
    public GameObject Prefab;
}

[Serializable]
public class SkillConfigData
{
    public SkillEnum Name;
    public GameObject Prefab;
}

[Serializable]
public class BuffConfigData
{
    public BuffEnum Name;
    public GameObject Prefab;
}

[CreateAssetMenu(fileName = "DataAssets", menuName = "ScripableObjects/DataAssets", order = 0)]
public class DataAssets : ScriptableObject
{
    [Header("===== Characters Config Data =====")]
    [SerializeField] 
    private List<CharacterConfigData> _configCharactersStatList;
    public List<CharacterConfigData> ConfigCharactersStatList => _configCharactersStatList;

    [Space (20)]

    [Header("===== Enemies Config Data =====")]
    [SerializeField]
    private List<EnemyConfigData> _configEnemiesStatList;
    public List<EnemyConfigData> ConfigEnemiesStatList => _configEnemiesStatList;

    [Space(20)]

    [Header("===== Skills Config Data =====")]
    [SerializeField]
    private List<SkillConfigData> _configSkillsStatList;
    public List <SkillConfigData> ConfigSkillsStatList => _configSkillsStatList;

    [Header("===== Buffs Config Data =====")]
    [SerializeField]
    private List<BuffConfigData> _configBuffsDataList;
    public List<BuffConfigData> ConfigBuffsDataList => _configBuffsDataList;

    public CharacterConfigData GetCharacterConfig(CharacterEnum characterName)
    {
        return _configCharactersStatList.Find(x => x.Name == characterName);
    }

    public EnemyConfigData GetEnemyConfig(EnemyEnum enemyName)
    {
        return _configEnemiesStatList.Find(x => x.Name == enemyName);
    }
    public EnemyConfigData GetEnemyConfigByRank(EnemyRank rank)
    {
        return _configEnemiesStatList.Find(x => x.Rank == rank);
    }
    public SkillConfigData GetSkillConfig(SkillEnum skillName)
    {
        return _configSkillsStatList.Find(x => x.Name == skillName);
    }

    public BuffConfigData GetBuffConfig(BuffEnum buffName)
    {
        return _configBuffsDataList.Find(x => x.Name == buffName);
    }
}
