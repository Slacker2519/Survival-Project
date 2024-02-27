using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
