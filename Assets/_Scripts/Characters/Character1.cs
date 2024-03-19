using System.Collections.Generic;
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
        Debug.Log(Buffs.Count);
        
    }

    public override void InitCharacterStats(CharacterEnum name, long health, long defense, long damage, long speed,
        int critRate, long critDamage, long attackSpeed, long levelExpCap, float pickupRange)
    {
        CharStats.Level = 1;
        CharStats.Name = name;
        CharStats.Health = health;
        CharStats.Defense = defense;
        CharStats.Damage = damage;
        CharStats.Speed = speed;
        CharStats.CritRate = critRate;
        CharStats.CritDamage = critDamage;
        CharStats.AttackSpeed = attackSpeed;
        CharStats.LevelExpCap = levelExpCap;
        CharStats.PickupRange = pickupRange;
    }

    public override void TakeDamage(long damage)
    {
        CharStats.Health -= damage; 
        Debug.Log("Take Damage"+damage);
    }


}



