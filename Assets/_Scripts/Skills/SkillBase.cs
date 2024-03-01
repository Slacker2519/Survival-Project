using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public SkillEnum Name => _name;
    [SerializeField] private SkillEnum _name;

    public int Level => _level;
    [SerializeField] private int _level;
}
