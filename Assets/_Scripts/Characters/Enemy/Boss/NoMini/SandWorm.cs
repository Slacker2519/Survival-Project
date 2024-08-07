using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWorm : BaseEnemy
{
    public static Action OnChangeState;

    private int _roamingCounter = 0;

    protected override void Start()
    {
        //OnFinishDashAttack += IncreaseDashAttackCounter;
        GetDefaultValue();
    }

    #region Test
    [Space(5)]
    [Header("Test")]

    [SerializeField] private Transform _player;

    [SerializeField] private int _roamAttackNumber = 5;

    public Transform Player => _player;

    private void GetDefaultValue()
    {
        EnemyStat.Name = EnemyEnum.SandWorm;
        BaseStat.Health = 100f;
        BaseStat.Defense = 70f;
        BaseStat.Damage = 120f;
        BaseStat.Speed = 30f;
        EnemyStat.Rank = EnemyRank.Boss;

        ChangeState(EnemyStateEnum.Worm_RoamingState);
    }
    #endregion

    public override void InitEnemyStat(EnemyEnum name, float health, float defense, float damage, float speed, EnemyRank rank)
    {
        EnemyStat.Name = name;
        BaseStat.Health = health;
        BaseStat.Defense = defense;
        BaseStat.Damage = damage;
        BaseStat.Speed = speed;
        EnemyStat.Rank = rank;

        //ChangeState(EnemyStateEnum.RoamingState);
    }

    public override void TakeDamage(float damage)
    {

    }

    protected override void Behavior()
    {
        
    }

    public void SwitchFromCollideState()
    {
        if (_roamingCounter >= _roamAttackNumber)
        {
            ChangeState(EnemyStateEnum.Worm_Rising);
        }
        else
        {
            ChangeState(EnemyStateEnum.Worm_RoamingState);
        }
    }

    public void RoamingCounter()
    {
        _roamingCounter++;
    }

    public void RemoveFromScene()
    {
        _roamingCounter = 0;
        OnChangeState = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeState(EnemyStateEnum.Worm_CollideWall);
        }
    }

    private void OnDisable()
    {
        RemoveFromScene();
    }
}
