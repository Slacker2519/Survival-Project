using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : BaseBody
{
    private void Start()
    {
    }
    private void Update()
    {
        base.Move();


    }
    private void FixedUpdate()
    {
        this.PostEvent(EventID.OnPlayerMove, this.transform);
    }

    public override void TakeDamage(long dame)
    {
        base.TakeDamage(dame);
        this.PostEvent(EventID.OnPlayerTakeDamage);
    }
}

