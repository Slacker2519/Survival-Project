using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBody : MonoBehaviour
{
    public Dictionary<BuffEnum, Ability> Buffs = new Dictionary<BuffEnum, Ability>();
    public Dictionary<DeBuffEnum, IDebuff> DeBuffs = new Dictionary<DeBuffEnum, IDebuff>();


    [SerializeField] private BaseBodyStat baseStat;

    public BaseBodyStat BaseStat { get => baseStat; set => baseStat = value; }

    public abstract void TakeDamage(long damage);
}

[Serializable]
public sealed class BaseBodyStat
{
    public long Health;
    public long Defense;
    public long Damage;
    public long Speed;
}
