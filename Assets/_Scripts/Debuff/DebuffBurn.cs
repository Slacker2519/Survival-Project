using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffBurn : DebuffBase
{
    Coroutine BurnEffet;
    float interval;
    float time;
    float tempTime;
    void Start()
    {

    }
    public override void Execute(object data)
    {

        if (Body == null)
        {
            Body = FindObjectOfType<BaseCharacter>();
        }
        if (BurnEffet != null)
        {
            //StopCoroutine(BurnEffet);
            //BurnEffet = null;
            tempTime = time;

        }
        else
        {
            time = 10;
            interval = 0.5f;
            tempTime = time;
            BurnEffet = StartCoroutine(Burn(30,interval));
        }
    }
    public override GameObject ReturnGameObject()
    {
        return gameObject;
    }

    public override void Upgrade()
    {
        Level++;
    }
    private void ResetValue()
    {
        Level = 1;
        BurnEffet = null;
    }
    IEnumerator Burn(long burnDame, float interval)
    {
        while (tempTime >= 0)
        {
            Debug.Log("Chays chays");
            Body.TakeDamage(burnDame);
            //Body.BaseStat.Health -= burnDame;
            tempTime -= interval;
            yield return new WaitForSeconds(interval);
        }
        yield return null;
        ResetValue();
        StopAllCoroutines();
    }
}
