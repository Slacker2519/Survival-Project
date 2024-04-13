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
        
    }

    public override void InitCharacterStats(CharacterEnum name, long health, long defense, long damage, long speed,
        int critRate, long critDamage, long attackSpeed, long levelExpCap, float pickupRange)
    {
        BaseStat.Health = health;
        BaseStat.Defense = defense;
        BaseStat.Damage = damage;
        BaseStat.Speed = speed;
        CharStats.Level = 1;
        CharStats.Name = name;
        CharStats.CritRate = critRate;
        CharStats.CritDamage = critDamage;
        CharStats.AttackSpeed = attackSpeed;
        CharStats.LevelExpCap = levelExpCap;
        CharStats.PickupRange = pickupRange;
    }

    public override void TakeDamage(long damage)
    {

        BaseStat.Health -= damage;
        if(BaseStat.Health<0) BaseStat.Health = 0;
        //Debug.Log(CharStats.Health);
        this.PostEvent(EventID.OnPlayerTakeDamage);
        Debug.Log("Take Damage"+damage);
    }


}



