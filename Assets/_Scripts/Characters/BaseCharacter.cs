using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public Dictionary<BuffEnum, Ability> Buffs = new Dictionary<BuffEnum, Ability>();
    protected Rigidbody2D _Rigidbody2d;

    public CharacterData CharStats => _CharacterStat;
    [SerializeField] protected CharacterData _CharacterStat;

    public bool IsWalking => _isWalking;
    private bool _isWalking;

    public abstract void InitCharacterStats(CharacterEnum name, long health, long defense, long damage, long speed,
        int critRate, long critDamage, long attackSpeed, long levelExpCap, float pickupRange);

    protected void Move()
    {
        GameController gameController = GameManager.Instance.Controller;
        Vector2 inputVector = gameController.PlayerInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        _Rigidbody2d.MovePosition(_Rigidbody2d.position + (moveDir.normalized * _CharacterStat.Speed * Time.fixedDeltaTime));
        _isWalking = moveDir != Vector2.zero;
    }
}
