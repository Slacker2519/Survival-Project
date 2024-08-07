using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingState : IStateMachine<BaseEnemy>
{
    private Vector3 _risingPos;

    public void EnterState(BaseEnemy t)
    {
        _risingPos = Vector3.zero;

        SandWorm worm = t.GetComponent<SandWorm>();
        if (worm != null)
        {
            _risingPos = worm.Player.transform.position;
        }
    }

    public void UpdateState(BaseEnemy t)
    {
        SandWorm worm = t.GetComponent<SandWorm>();
        if (worm != null)
        {
            Vector3 wormPos = worm.transform.position;
            worm.transform.position = Vector3.MoveTowards(wormPos, _risingPos, worm.BaseStat.Speed * Time.deltaTime);
        }
    }

    public void ExitState(BaseEnemy t)
    { 
        
    }
}
