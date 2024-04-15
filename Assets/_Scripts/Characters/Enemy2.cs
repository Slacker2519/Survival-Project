using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : BaseEnemy
{
    private void Start()
    {
        _CurrentCoolDown = _ResetPosCoolDown;
    }

    private void Update()
    {
        ChasePlayer();
        ResetEnemyPos();
    }

    public override void InitEnemyStat(EnemyEnum name, long health, long defense, long damage, long speed, EnemyRank rank)
    {
        EnemyStat.Name = name;
        EnemyStat.Health = health;
        EnemyStat.Defense = defense;
        EnemyStat.Damage = damage;
        EnemyStat.Speed = speed;
        EnemyStat.Rank = rank;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet == null)
            {
                return;
            }
            if (!bullet.DamageValue.damageAble)
            {
                return;
            }
            long damage = bullet.DamageValue.value;
            EnemyStat.Health -= damage;
            if (EnemyStat.Health < 0)
            {
                PoolManager.Instance.ReturnObject(this.gameObject, name.ToString());
            }

        }
    }
}
