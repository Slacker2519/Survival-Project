using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] DamageValue damageValue;

    public DamageValue DamageValue { get => damageValue; set => damageValue = value; }

    public void Init(DamageType damageType, int _damage = 0, bool _damageAble = true) { DamageValue = new DamageValue(damageType, _damageAble, _damage); }

    public virtual void SelfDestruct() { }
}
