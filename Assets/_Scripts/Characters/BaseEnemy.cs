using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseBody
{
    [SerializeField] protected float _minDistanceFollow = 1.5f;

    public void InitCharacterStats(EnemyEnum name, long health, long defense, long damage, long speed)
    {
        Stats = new CharacterStats(name, health, defense, damage, speed);
    }

    protected virtual void ChasePlayer()
    {
        Vector3 PlayerPos = GameManager.Instance.Controller.Player.transform.position;

        if (Vector3.Distance(transform.position, PlayerPos) > _minDistanceFollow)
        {
            Vector3 direction = PlayerPos - transform.position;
            Vector3 normDic = direction.normalized;
            transform.position += normDic * Stats.Speed * Time.deltaTime;
        }
    }
}
