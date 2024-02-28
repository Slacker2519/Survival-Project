using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public CharacterStats CharStats => Stats;
    protected CharacterStats Stats;

    [SerializeField] protected Rigidbody2D _rigidbody2d;
    [SerializeField] protected float RotateSpeed;

    public bool IsWalking => _isWalking;
    private bool _isWalking;

    // Initiate character stats when spawn character from poolmanager or gamecontroller
    public void InitCharacterStats(CharacterEnum name, long health, long defense, long damage, long speed)
    {
        Stats = new CharacterStats(name, health, defense, damage, speed);
    }

    protected void MoveCharacter()
    {
        GameController gameController = GameManager.Instance.Controller;
        Vector2 inputVector = gameController.Input.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        _rigidbody2d.MovePosition(_rigidbody2d.position + (moveDir.normalized * Stats.Speed * Time.fixedDeltaTime));
        _isWalking = moveDir != Vector2.zero;

        //Vector2 mouseWordPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //float angle = Mathf.Atan2(mouseWordPos.y, mouseWordPos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //Modify by Tuan Anh 23/02/2024.
    public virtual void TakeDamage(long dame)
    {
        long trueDame = dame - Stats.Defense;
        trueDame = trueDame > 0 ? trueDame : 0;
        Stats.Health -= trueDame;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
