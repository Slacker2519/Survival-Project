using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWorm : BaseEnemy
{
    public static Action OnFinishDashAttack;

    private int _dashAttackCounter = 0;

    protected override void Start()
    {
        OnFinishDashAttack += IncreaseDashAttackCounter;
    }

    public override void InitEnemyStat(EnemyEnum name, float health, float defense, float damage, float speed, EnemyRank rank)
    {
        EnemyStat.Name = name;
        BaseStat.Health = health;
        BaseStat.Defense = defense;
        BaseStat.Damage = damage;
        BaseStat.Speed = speed;
        EnemyStat.Rank = rank;

        ChangeState(EnemyStateEnum.RunTowardPlayer);
    }

    public override void TakeDamage(float damage)
    {
    }

    protected override void Behavior()
    {

    }

    private void IncreaseDashAttackCounter()
    {
        _dashAttackCounter++;
    }

    public void RemoveFromScene()
    {
        _dashAttackCounter = 0;
        OnFinishDashAttack -= IncreaseDashAttackCounter;
    }

    private void OnDisable()
    {
        RemoveFromScene();
    }
}
