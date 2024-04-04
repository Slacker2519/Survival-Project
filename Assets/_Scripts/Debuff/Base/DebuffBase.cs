using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DebuffBase : MonoBehaviour, IDebuff
{

    [SerializeField] protected DeBuffEnum Name;
    [SerializeField] protected int Level;
    [SerializeField] protected object Body;
    public abstract void Execute(object data);

    public abstract GameObject ReturnGameObject();

    public abstract void Upgrade();
}
