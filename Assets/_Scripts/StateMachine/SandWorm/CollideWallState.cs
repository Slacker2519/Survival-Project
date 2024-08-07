using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWallState : IStateMachine<BaseEnemy>
{
    private const float _shakeDuration = 3f;

    public void EnterState(BaseEnemy t)
    {
        SandWorm worm = t.GetComponent<SandWorm>();
        if (worm != null)
        {
            worm.transform.DOShakePosition(_shakeDuration)
                .OnComplete(() =>
                {
                    worm.SwitchFromCollideState();
                });
        }
    }

    public void UpdateState(BaseEnemy t)
    {

    }

    public void ExitState(BaseEnemy t)
    {
        t.transform.DOKill();
    }
}
