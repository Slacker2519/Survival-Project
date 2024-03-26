using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharDebuffBase : MonoBehaviour, IDebuff
{

    [SerializeField] protected DeBuffEnum Name;
    [SerializeField] protected int Level;
    [SerializeField] protected BaseCharacter BaseChar;
    public abstract void Execute(object data);

    public abstract GameObject ReturnGameObject();

    public abstract void Upgrade();
}
