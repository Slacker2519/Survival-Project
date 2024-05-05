using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laucent : BaseEnemy
{
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();

        Colliderr = GetComponent<Collider2D>();

        _CurrentCoolDown = _ResetPosCoolDown;
    }

    private void OnEnable()
    {

    }

    protected override void Start()
    {

    }

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
        throw new System.NotImplementedException();
    }
}
