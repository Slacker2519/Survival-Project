using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IStateMachine<BaseEnemy>
{
    public void EnterState(BaseEnemy t)
    {
        t.CanChangeState = false;
    }

    public void UpdateState(BaseEnemy t)
    {
        // play attack animation
    }

    public void ExitState(BaseEnemy t)
    {

    }
}
