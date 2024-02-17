using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;

    public CharacterStats(long health, long defense, long damage, long speed)
    {
        Health = health;
        Defense = defense;
        Damage = damage;
        Speed = speed;
    }
}
