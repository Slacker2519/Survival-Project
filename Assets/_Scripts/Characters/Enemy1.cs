public class Enemy1 : BaseEnemy
{
    private void Start()
    {
        _CurrentCoolDown = _ResetPosCoolDown;
    }

    private void Update()
    {
        ChasePlayer();
        ResetEnemyPos();
    }

    public override void InitEnemyStat(EnemyEnum name, long health, long defense, long damage, long speed)
    {
        EnemyStat.Name = name;
        EnemyStat.Health = health;
        EnemyStat.Defense = defense;
        EnemyStat.Damage = damage;
        EnemyStat.Speed = speed;
    }
}
