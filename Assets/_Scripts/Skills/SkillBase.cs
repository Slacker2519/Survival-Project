using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour,Ability
{
    /// <summary>
    /// Type of skill
    /// </summary>
    public SkillEnum Name => _name;

    public int Level { get => _level; set => _level = value; }

    [SerializeField] private SkillEnum _name;

    [SerializeField] private int _level;
    public abstract void Execute();

    public abstract GameObject ReturnGameObject();

    public abstract void Upgrade();
}
