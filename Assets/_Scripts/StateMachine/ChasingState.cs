using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChasingState : IStateMachine<BaseEnemy>
{
    public void EnterState(BaseEnemy t)
    {
        t.CanChangeState = true;
    }

    public void UpdateState(BaseEnemy t)
    {
        Vector3 dirMove = (GameManager.Instance.Controller.Player.transform.position - t.transform.position).normalized;

        t.RB.isKinematic = false;
        t.Colliderr.isTrigger = false;
        t.RB.velocity = dirMove * t.BaseStat.Speed;
    }

    public void ExitState(BaseEnemy t)
    {
        t.RB.velocity = Vector3.zero;
        t.RB.isKinematic = true;
        //t.Colliderr.isTrigger = true;
    }
}
