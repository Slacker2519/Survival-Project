using UnityEngine;

public class Enemy1 : BaseEnemy
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

    public override void InitEnemyStat(EnemyEnum name, long health, long defense, long damage, long speed)
    {
        EnemyStat.Name = name;
        EnemyStat.Health = health;
        EnemyStat.Defense = defense;
        EnemyStat.Damage = damage;
        EnemyStat.Speed = speed;
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
            if (!bullet.damageAble)
            {
                return;
            }
            long damage = bullet.damage;
            EnemyStat.Health -= damage;
            if (EnemyStat.Health < 0)
            {
                PoolManager.Instance.ReturnObject(this.gameObject, name.ToString());
            }

        }
    }

}
