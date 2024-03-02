using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public int Level;
    public CharacterEnum Name;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;

    public CharacterData(int level, CharacterEnum name, long health, long defense, long damage, long speed)
    {
        Level = level;
        Name = name;
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
    }
}
