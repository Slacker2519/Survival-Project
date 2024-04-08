using UnityEngine;

public abstract class BaseEnemy : BaseBody<EnemyData>
{
    protected Rigidbody2D _Rigidbody2d;

    public EnemyData EnemyStat => Stats;
    //[SerializeField] protected EnemyData _EnemyStat;
    [Space(10)]
    [SerializeField] protected float _MinDistanceFollow = 1.5f;
    [SerializeField] protected float _MaxDistanceFollow = 10;
    [SerializeField] protected float _ResetPosCoolDown = 2f;
    protected float _CurrentCoolDown;

    public abstract void InitEnemyStat(EnemyEnum name, long health, long defense, long damage, long speed,EnemyRank rank);

    protected virtual void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.Controller.Player.transform.position) > _MinDistanceFollow)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.Controller.Player.transform.position, EnemyStat.Speed * Time.deltaTime);
        }
    }

    protected void ResetEnemyPos()
    {
        _CurrentCoolDown -= Time.deltaTime;
        if (_CurrentCoolDown > 0f) return;
        if (Vector3.Distance(transform.position, GameManager.Instance.Controller.Player.transform.position) > _MaxDistanceFollow)
        {
            transform.position = GameManager.Instance.Controller.GetEnemySpawnedPos();
            _CurrentCoolDown = _ResetPosCoolDown;
        }
    }
}
