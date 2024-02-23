using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : BaseCharacter
{

    private void Start()
    {
        this.RegisterEvent(EventID.OnPlayerTakeDamage,WhenPlayerTakeDame);
        TakeDamage(70);
    }
    private void Update()
    {
        MoveCharacter();
    }

    public override void TakeDamage(long dame)
    {
        base.TakeDamage(dame);
        this.PostEvent(EventID.OnPlayerTakeDamage);
    }

    /// <summary>
    /// Example event excute when player take damage.
    /// </summary>
    public void WhenPlayerTakeDame(object a)
    {

    }

}
