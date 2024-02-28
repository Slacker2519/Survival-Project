using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    // Data need to save
    public CharacterStats CharacterPicked;

    public GameData()
    {
        var stat = ConfigDataManager.Instance.DataAssets.GetCharacterConfig(CharacterEnum.Character1);
        CharacterPicked = new CharacterStats(stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed);
    }
}

[Serializable]
public class CharacterStats
{
    public CharacterEnum Name;
    public EnemyEnum EnemyName;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;

    public CharacterStats(CharacterEnum name, long health, long defense, long damage, long speed)
    {
        Name = name;
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
    }

    public CharacterStats(EnemyEnum name, long health, long defense, long damage, long speed)
    {
        EnemyName = name;
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
    }
}
