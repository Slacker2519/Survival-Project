using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBody : MonoBehaviour
{
    public CharacterStats CharStats => _CharacterStat;
    [SerializeField] protected CharacterStats _CharacterStat;

    [Space (10)]

    [SerializeField] protected Rigidbody2D _Rigidbody2d;
    [SerializeField] protected float _RotateSpeed;

    public bool IsWalking => _isWalking;
    private bool _isWalking;

    // Initiate character stats when spawn character from poolmanager or gamecontroller
    public void InitCharacterStats(int level, CharacterEnum name, long health, long defense, long damage, long speed)
    {
        _CharacterStat = new CharacterStats(level, name, health, defense, damage, speed);
    }

    protected virtual void Move()
    {
        GameController gameController = GameManager.Instance.Controller;
        Vector2 inputVector = gameController.Input.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        _Rigidbody2d.MovePosition(_Rigidbody2d.position + (moveDir.normalized * _CharacterStat.Speed * Time.fixedDeltaTime));
        _isWalking = moveDir != Vector2.zero;

        //Vector2 mouseWordPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //float angle = Mathf.Atan2(mouseWordPos.y, mouseWordPos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //Modify by Tuan Anh 23/02/2024.
    public virtual void TakeDamage(long dame)
    {
        long trueDame = dame - _CharacterStat.Defense;
        trueDame = trueDame > 0 ? trueDame : 0;
        _CharacterStat.Health -= trueDame;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
