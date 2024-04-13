using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DebuffBase : MonoBehaviour, IDebuff
{

    public DeBuffEnum Name;
    public int Level;
    public BaseBody Body;
    public abstract void Execute(object data);

    public abstract GameObject ReturnGameObject();

    public abstract void Upgrade();
}
