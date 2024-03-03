using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour,Ability
{
    /// <summary>
    /// Type of skill
    /// </summary>
    public SkillEnum Name => _name;
    [SerializeField] private SkillEnum _name;

    public int Level => _level;
    [SerializeField] private int _level;
    public abstract void Execute();

    public abstract GameObject ReturnGameObject();
}
