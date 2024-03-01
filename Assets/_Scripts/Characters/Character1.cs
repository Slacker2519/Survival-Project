using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : BaseBody
{
    private void Awake()
    {
        _Rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        base.Move();
        this.PostEvent(EventID.OnPlayerMove, this.transform);
    }

    public override void TakeDamage(long dame)
    {
        base.TakeDamage(dame);
        this.PostEvent(EventID.OnPlayerTakeDamage);
    }
}

