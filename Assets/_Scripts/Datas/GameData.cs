using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // Data need to save
    public CharacterStats CharacterPicked;

    public GameData(DataAssets dataAssets)
    {
        var stat = dataAssets.GetCharacterConfig(CharacterEnum.Character1);
        CharacterPicked = new CharacterStats(stat.Level, stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed);
    }
}

[Serializable]
public class CharacterStats
{
    public int Level;
    public CharacterEnum Name;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;

    public CharacterStats(int level, CharacterEnum name, long health, long defense, long damage, long speed)
    {
        Level = level;
        Name = name;
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
    }
}

[Serializable]
public class EnemyStats
{
    public EnemyEnum Name;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;

    public EnemyStats(EnemyEnum name, long health, long defense, long damage, long speed)
    {
        Name = name;
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
    }
}
