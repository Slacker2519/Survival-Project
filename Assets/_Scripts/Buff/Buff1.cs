using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff1 : BuffBase
{
    [SerializeField] private Bullet_Buff bullet;
    [SerializeField] private List<Bullet_Buff> BulletsPool = new List<Bullet_Buff>();

    public override void Execute()
    {
        StartCoroutine(nameof(AutoShoot));
    }

    public override GameObject ReturnGameObject()
    {
        return this.gameObject;
    }

    public override void Upgrade()
    {
        Level += 1;
        Debug.Log(Level);
    }

    IEnumerator AutoShoot()
    {
        while (true)
        {
            float angle = 0;
            for (int i = 0; i < 20; i++)
            {
                float anglerad = Mathf.Deg2Rad * angle;
                float x = transform.position.x + 2 * Mathf.Sin( anglerad);
                float y = transform.position.y + 2 * Mathf.Cos( anglerad);
                Vector3 posBullet = new Vector2(x, y);
                var bullet = SpawnBullet(posBullet, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = (posBullet - transform.position).normalized * 10;
                angle += 360 / 20;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Bullet_Buff SpawnBullet(Vector3 position, Quaternion quaternion)
    {
        Bullet_Buff obj = BulletsPool.Find(x => !x.gameObject.activeSelf);
        if (obj == null)
        {
            obj = Instantiate(bullet);
            BulletsPool.Add(obj);
        }
        obj.transform.position = position;
        obj.transform.rotation = quaternion;
        obj.gameObject.SetActive(true);
        return obj;
    }
}
