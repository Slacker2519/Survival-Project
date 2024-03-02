using UnityEngine;

public class Character1 : BaseCharacter
{
    private void Awake()
    {
        _Rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        this.PostEvent(EventID.OnPlayerMove, this.transform);
    }

    public override void InitCharacterStats(int level, CharacterEnum name, long health, long defense, long damage, long speed)
    {
        CharStats.Level = level;
        CharStats.Name = name;
        CharStats.Health = health;
        CharStats.Defense = defense;
        CharStats.Damage = damage;
        CharStats.Speed = speed;
    }
}

