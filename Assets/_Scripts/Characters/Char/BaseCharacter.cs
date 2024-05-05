using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : BaseBody
{
    protected Rigidbody2D _Rigidbody2d;

    public CharacterData CharStats => _CharacterStat;
    [SerializeField] protected CharacterData _CharacterStat;

    public bool IsWalking => _isWalking;
    private bool _isWalking;

    public abstract void InitCharacterStats(CharacterEnum name, float health, float defense, float damage, float speed,
        float critRate, float critDamage, float attackSpeed, float levelExpCap, float pickupRange);
    public abstract override void TakeDamage(float damage);
    protected void Move()
    {
        GameController gameController = GameManager.Instance.Controller;
        Vector2 inputVector = gameController.PlayerInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        _Rigidbody2d.MovePosition(_Rigidbody2d.position + (moveDir.normalized * BaseStat.Speed * Time.fixedDeltaTime));
        _isWalking = moveDir != Vector2.zero;
    }

}
