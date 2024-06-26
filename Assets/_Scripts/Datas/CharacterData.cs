using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public int Level;
    public CharacterEnum Name;
    public float CritRate;
    public float CritDamage;
    public float AttackSpeed;
    public float LevelExpCap;
    public float CurrentExp;
    public float PickupRange;

    public CharacterData(CharacterEnum name, float critRate, float critDamage, float attackSpeed, float levelExpCap, float pickupRange)
    {
        Level = 1;
        Name = name;
        CritRate = critRate;
        CritDamage = critDamage;
        AttackSpeed = attackSpeed;
        LevelExpCap = levelExpCap;
        CurrentExp = 0;
        PickupRange = pickupRange;
    }
}
