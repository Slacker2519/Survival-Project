using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterEnum Name;
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;

    public CharacterStats(CharacterEnum name, long health, long defense, long damage, long speed)
    {
        Name = name;
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
    }
}
