using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public EnemyEnum Name;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;

    public EnemyData(EnemyEnum name, long health, long defense, long damage, long speed)
    {
        Name = name;
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
    }
}
