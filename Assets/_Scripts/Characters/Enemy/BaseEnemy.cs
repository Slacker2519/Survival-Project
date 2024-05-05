using System;
using UnityEngine;

public abstract class BaseEnemy : BaseBody
{
    protected Action DoReadyToAttack;

    //public AttackState AttackState => _AttackState;
    //protected AttackState _AttackState;

    public Rigidbody2D RB { get => rigidbody2d; set => rigidbody2d = value; }
    private Rigidbody2D rigidbody2d;

    public Collider2D Colliderr { get => colliderr; set => colliderr = value; }
    private Collider2D colliderr;

    public EnemyData EnemyStat => _EnemyStat;
    [SerializeField] protected EnemyData _EnemyStat;

    private EnemyStateEnum _stateName;

    public IStateMachine<BaseEnemy> CurrentState;
    [SerializeField] private IStateMachine<BaseEnemy> _currentState;

    public bool CanChangeState 
    { 
        get { return _canChangeState; } 
        set { _canChangeState = value; }
    }
    private bool _canChangeState;

    [SerializeField] protected float _MinDistanceFollow = 1.5f;
    [SerializeField] protected float _MaxDistanceFollow = 10;
    [SerializeField] protected float _ResetPosCoolDown = 2f;
    protected float _CurrentCoolDown;

    //[SerializeField] protected SpriteRenderer _SpriteRenderer;
    //[SerializeField] protected float _AttackDuration;
    //protected float _CurrentAttackDuration;
    //[SerializeField] protected float _RestDuration;
    //protected float _CurrentRestDuration;

    public bool DealDamage => _DealDamage;
    protected bool _DealDamage;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }

        Behavior();
    }

    public abstract void InitEnemyStat(EnemyEnum name, float health, float defense, float damage, float speed,EnemyRank rank);

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

    protected virtual void Behavior()
    {
        if (!_canChangeState) return;

        if (Vector3.Distance(transform.position, GameManager.Instance.Controller.Player.transform.position) > _MinDistanceFollow)
        {
            ChangeState(EnemyStateEnum.Chasing);
        }
        else
        {
            ChangeState(EnemyStateEnum.Attacking);
        }
    }

    //protected virtual void ChasePlayer()
    //{
    //    if (Vector3.Distance(transform.position, GameManager.Instance.Controller.Player.transform.position) > _MinDistanceFollow)
    //    {
    //        Vector3 dirMove = (GameManager.Instance.Controller.Player.transform.position - transform.position).normalized;

    //        RB.isKinematic = false;
    //        Colliderr.isTrigger = false;
    //        RB.velocity = dirMove * 2; //BaseStat.Speed;
    //        SwitchState(EnemyState.CantAttack, () =>
    //        {
    //            _AttackState = AttackState.None;
    //        });
    //    }
    //    else
    //    {
    //        RB.velocity = Vector3.zero;
    //        RB.isKinematic = true;
    //        Colliderr.isTrigger = true;
    //        SwitchState(EnemyState.CanAttack, DoReadyToAttack);
    //    }
    //}

    #region Enemy state machine

    public virtual void ChangeState(EnemyStateEnum state)
    {
        if (_stateName == state) return;

        _stateName = state;

        if (state == EnemyStateEnum.None) return;

        if (_currentState != null)
        {
            _currentState.ExitState(this);
        }

        var newState = PoolManager.Instance.GetState(state);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public void EndState()
    {
        if (_currentState != null)
        {
            _currentState.ExitState(this);
            _currentState = null;
        }
    }

    public virtual void ReturnToPool()
    {
        _stateName = EnemyStateEnum.None;
        EndState();
        PoolManager.Instance.ReturnObject(gameObject);
    }

    #endregion
}
