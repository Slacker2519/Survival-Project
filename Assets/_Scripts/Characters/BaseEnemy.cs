using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public CharacterStats CharStats => Stats;
    protected CharacterStats Stats;

    [SerializeField] protected Rigidbody2D _rigidbody2d;
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

    public virtual void TakeDamage(long dame)
    {
        long trueDame = dame - Stats.Defense;
        trueDame = trueDame > 0 ? trueDame : 0;
        Stats.Health -= trueDame;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
