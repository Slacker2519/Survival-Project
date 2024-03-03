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
    public int CritRate;
    public long CritDamage;
    public long AttackSpeed;
    public long LevelExpCap;
    public long CurrentExp;
    public float PickupRange;

    public CharacterData(CharacterEnum name, long health, long defense, long damage, long speed, 
        int critRate, long critDamage, long attackSpeed, long levelExpCap, float pickupRange)
    {
        Level = 1;
        Name = name;
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
        CritRate = critRate;
        CritDamage = critDamage;
        AttackSpeed = attackSpeed;
        LevelExpCap = levelExpCap;
        CurrentExp = 0;
        PickupRange = pickupRange;
    }
}
