using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : BaseCharacter
{
    private void Start()
    {
    }
    private void Update()
    {
        MoveCharacter();


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

