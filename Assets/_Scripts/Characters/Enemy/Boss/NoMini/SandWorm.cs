using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWorm : BaseEnemy
{
    public override void InitEnemyStat(EnemyEnum name, float health, float defense, float damage, float speed, EnemyRank rank)
    {
        EnemyStat.Name = name;
        BaseStat.Health = health;
        BaseStat.Defense = defense;
        BaseStat.Damage = damage;
        BaseStat.Speed = speed;
        EnemyStat.Rank = rank;

        ChangeState(EnemyStateEnum.Chasing);
    }

    public override void TakeDamage(float damage)
    {
    }

    protected override void Start()
    {
        
    }

    //protected override void Update()
    //{
        
    //}

    protected override void Behavior()
    {
    }
}
