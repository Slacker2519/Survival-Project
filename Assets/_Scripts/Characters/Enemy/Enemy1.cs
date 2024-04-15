using UnityEngine;

public class Enemy1 : BaseEnemy
{
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Colliderr = GetComponent<Collider2D>();
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
        BaseStat.Health = health;
        BaseStat.Defense = defense;
        BaseStat.Damage = damage;
        BaseStat.Speed = speed;
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
            BaseStat.Health -= damage;
            if (BaseStat.Health < 0)
            {
                PoolManager.Instance.ReturnObject(this.gameObject, name.ToString());
            }

        }
    }

    public override void TakeDamage(long damage)
    {
        //throw new System.NotImplementedException();
    }
}
