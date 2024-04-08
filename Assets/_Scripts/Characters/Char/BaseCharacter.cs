using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : BaseBody<CharacterData>
{
    protected Rigidbody2D _Rigidbody2d;

    public CharacterData CharStats => Stats;
    //[SerializeField] protected CharacterData _CharacterStat;

    public bool IsWalking => _isWalking;
    private bool _isWalking;

    public abstract void InitCharacterStats(CharacterEnum name, long health, long defense, long damage, long speed,
        int critRate, long critDamage, long attackSpeed, long levelExpCap, float pickupRange);
    public abstract override void TakeDamage(long damage);
    protected void Move()
    {
        GameController gameController = GameManager.Instance.Controller;
        Vector2 inputVector = gameController.PlayerInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        _Rigidbody2d.MovePosition(_Rigidbody2d.position + (moveDir.normalized * CharStats.Speed * Time.fixedDeltaTime));
        _isWalking = moveDir != Vector2.zero;
    }

}
