using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBody : MonoBehaviour
{
    public Dictionary<BuffEnum, Ability> Buffs = new Dictionary<BuffEnum, Ability>();
    public Dictionary<DeBuffEnum, IDebuff> DeBuffs = new Dictionary<DeBuffEnum, IDebuff>();

    public BaseBodyStat BaseStat { get => baseStat; set => baseStat = value; }
    [SerializeField] private BaseBodyStat baseStat;

    public abstract void TakeDamage(float damage);
}

[Serializable]
public sealed class BaseBodyStat
{
    public float Health;
    public float Defense;
    public float Damage;
    public float Speed;
}
