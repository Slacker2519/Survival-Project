using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDameCaulation : MonoBehaviour
{
    [SerializeField] BaseCharacter character;
    private void Awake()
    {

        character = this.GetComponent<BaseCharacter>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("trigger");
            BaseEnemy enemy = collision.GetComponent<BaseEnemy>();
            if (enemy == null)
            {
                return;
            }
            if (!enemy.DealDamage) return;

            float damage = enemy.BaseStat.Damage;
            character.TakeDamage(damage);
        }
    }
}
