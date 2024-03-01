using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseBody
{
    public EnemyStats EnemyStat => _EnemyStat;
    [SerializeField] protected EnemyStats _EnemyStat;

    [Space(10)]

    [SerializeField] protected float _MinDistanceFollow = 1.5f;

    public void InitEnemyStat(EnemyEnum name, long health, long defense, long damage, long speed)
    {
        _EnemyStat = new EnemyStats(name, health, defense, damage, speed);
    }

    protected virtual void ChasePlayer()
    {
        Vector3 PlayerPos = GameManager.Instance.Controller.Player.transform.position;

        if (Vector3.Distance(transform.position, PlayerPos) > _MinDistanceFollow)
        {
            Vector3 direction = PlayerPos - transform.position;
            Vector3 normDic = direction.normalized;
            transform.position += normDic * _EnemyStat.Speed * Time.deltaTime;
        }
    }
}
