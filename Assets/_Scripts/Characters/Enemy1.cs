using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    private void Update()
    {
        ChasePlayer();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        BaseCharacter body = collision.gameObject.GetComponent<BaseCharacter>();
        if (body == null) return;
        if (body.CharStats.Name.ToString().Contains("Character"))
        {
            body.TakeDamage(this.CharStats.Damage);
        }
    }

}
