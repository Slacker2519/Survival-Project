using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingState : IStateMachine<BaseEnemy>
{
    private Vector3 _dir;

    public void EnterState(BaseEnemy t)
    {
        SandWorm worm = t.GetComponent<SandWorm>();
        if (worm != null)
        {
            Vector3 dirUnnor = worm.Player.position - worm.transform.position;
            _dir = dirUnnor.normalized;
        }
    }

    public void UpdateState(BaseEnemy t)
    {
        SandWorm worm = t.GetComponent<SandWorm>();
        if (worm != null)
        {
            t.transform.position += _dir * t.BaseStat.Speed * Time.deltaTime;
        }
    }

    public void ExitState(BaseEnemy t)
    {
        SandWorm worm = t.GetComponent<SandWorm>();
        if (worm != null)
        {
            worm.RoamingCounter();
        }
    }
}
