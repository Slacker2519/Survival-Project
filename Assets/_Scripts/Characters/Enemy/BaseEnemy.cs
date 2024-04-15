using UnityEngine;

public abstract class BaseEnemy : BaseBody
{
    private Rigidbody2D rigidbody2d;
    private Collider2D colliderr;

    public EnemyData EnemyStat => _EnemyStat;

    public Rigidbody2D RB { get => rigidbody2d; set => rigidbody2d = value; }
    public Collider2D Colliderr { get => colliderr; set => colliderr = value; }

    [SerializeField] protected EnemyData _EnemyStat;
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
            Vector3 dirMove = (GameManager.Instance.Controller.Player.transform.position - transform.position).normalized;

            RB.isKinematic = false;
            Colliderr.isTrigger = false;
            RB.velocity = dirMove * 2; //BaseStat.Speed;
        }
        else
        {
            RB.velocity = Vector3.zero;
            RB.isKinematic = true;
            Colliderr.isTrigger = true;
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
