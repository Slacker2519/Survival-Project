using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected Rigidbody2D _Rigidbody2d;
    public EnemyData EnemyStat => _EnemyStat;
    [SerializeField] protected EnemyData _EnemyStat;

    [Space(10)]

    [SerializeField] protected float _MinDistanceFollow = 1.5f;

    public abstract void InitEnemyStat(EnemyEnum name, long health, long defense, long damage, long speed);

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
