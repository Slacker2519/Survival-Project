using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] DamageValue damageValue;

    public DamageValue DamageValue { get => damageValue; set => damageValue = value; }
    bool isInit;
    public void Init(DamageType damageType, int _damage = 0, bool _damageAble = true) { DamageValue = new DamageValue(damageType, _damageAble, _damage); }
    public void Setup(DamageType damageType, int _damage = 0, bool _damageAble = true)
    {
        if (!isInit)
        {
            Init(damageType, _damage, _damageAble);
            return;
        }
        DamageValue.damageAble = _damageAble;
        DamageValue.damageType = damageType;
        DamageValue.value = _damage;

    }

    public virtual void SelfDestruct() { }
}
