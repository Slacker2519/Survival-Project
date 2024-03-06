using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBase : MonoBehaviour, Ability
{
    public BuffEnum Name => _name;

    public int Level { get => _level; set => _level = value; }

    [SerializeField] private BuffEnum _name;

    [SerializeField] private int _level;

    public abstract void Execute();

    public abstract GameObject ReturnGameObject();

    public abstract void Upgrade();
}
