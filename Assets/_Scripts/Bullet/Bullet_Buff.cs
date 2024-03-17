using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Buff : Bullet 
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Return PoolganePoolManager.Instance.ReturnObject(this.gameObject,BuffEnum.Buff1.ToString());
            gameObject.SetActive(false);
        }
    }
}
