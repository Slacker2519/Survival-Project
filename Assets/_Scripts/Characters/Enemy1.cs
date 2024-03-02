using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    private void Update()
    {
        ChasePlayer();
    }
    public override void InitEnemyStat(EnemyEnum name, long health, long defense, long damage, long speed)
    {
        EnemyStat.Name = name;
        EnemyStat.Health = health;
        EnemyStat.Defense = defense;
        EnemyStat.Damage = damage;
        EnemyStat.Speed = speed;
    }
}
