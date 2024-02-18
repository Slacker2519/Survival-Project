using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
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
        Vector2 inputVector = GameController.Instance.Input.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        _rigidbody2d.MovePosition(_rigidbody2d.position + (moveDir.normalized * Stats.Speed * Time.fixedDeltaTime));
        _isWalking = moveDir != Vector2.zero;

        Vector2 mouseWordPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mouseWordPos.y, mouseWordPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
