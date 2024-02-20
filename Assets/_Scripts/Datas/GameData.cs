using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // Data need to save
}

[Serializable]
public class CharacterStats
{
    public CharacterEnum Name;
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
}
