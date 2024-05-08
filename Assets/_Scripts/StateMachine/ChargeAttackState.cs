using UnityEngine;

public class ChargeAttackState : IStateMachine<BaseEnemy>
{
    private float _timeHoldCharge = 10;

    private float _currentTimeHoldCharge;

    private float _distanceCharge = 10;

    private float _chargeSpeed = 3;

    public void EnterState(BaseEnemy t)
    {
        t.CanChangeState = false;

        _currentTimeHoldCharge = _timeHoldCharge;
    }

    public void UpdateState(BaseEnemy t)
    {
        _currentTimeHoldCharge -= Time.deltaTime;
        Vector3 direction = Vector3.zero;

        if (_currentTimeHoldCharge < 0 )
        {
            _currentTimeHoldCharge = _timeHoldCharge;

            direction = (GameManager.Instance.Controller.Player.transform.position - t.transform.position).normalized;
        }

        Vector3 chargeDestination = t.transform.position + direction * _distanceCharge;

        if (Vector3.Distance(t.transform.position, chargeDestination) <= 0.5f)
        {
            t.RB.isKinematic = false;
            t.Colliderr.isTrigger = false;
            t.RB.velocity = direction * _chargeSpeed;
        }
    }

    public void ExitState(BaseEnemy t)
    {
        t.RB.velocity = Vector3.zero;
        t.RB.isKinematic = true;
    }
}
