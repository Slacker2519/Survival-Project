using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDebuffBurn : CharDebuffBase
{
    Coroutine BurnEffet;
    void Start()
    {
        BaseChar = FindObjectOfType<BaseCharacter>();
    }
    public override void Execute(object data)
    {
        if (BurnEffet != null)
        {
            StopCoroutine(BurnEffet);
            BurnEffet = null;
        }
        else
        {
            BurnEffet = StartCoroutine(Burn(1));
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
    IEnumerator Burn(long burnDame, float interval = 0.1f, float time = 5f)
    {
        if(!BaseChar) yield break;
        float tempTime = time;
        while (tempTime>=0)
        {
            BaseChar.CharStats.Health -= burnDame;
            tempTime -= Time.deltaTime;
            yield return null;
        }
        yield return null;
        ResetValue();
        StopAllCoroutines();
    }
}
