using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    protected Rigidbody2D _Rigidbody2d;
    public CharacterStats CharStats => _CharacterStat;
    [SerializeField] protected CharacterStats _CharacterStat;
    public abstract void InitCharacterStats(int level, CharacterEnum name, long health, long defense, long damage, long speed);
}
