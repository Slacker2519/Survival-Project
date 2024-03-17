using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public long damage;
    public bool damageAble;
    public void Init(int _damage = 0,bool _damageAble = true) { damage = _damage; damageAble = _damageAble; }




    public virtual void SelfDestruct() { }
}
