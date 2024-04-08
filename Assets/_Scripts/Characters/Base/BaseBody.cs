using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBody<T> : MonoBehaviour where T : class
{
    public Dictionary<BuffEnum, Ability> Buffs = new Dictionary<BuffEnum, Ability>();
    public Dictionary<DeBuffEnum, Ability> DeBuffs = new Dictionary<DeBuffEnum, Ability>();

    [SerializeField] T _stats;

    public T Stats { get => _stats; set => _stats = value; }
    public abstract void TakeDamage(long damage);
}
