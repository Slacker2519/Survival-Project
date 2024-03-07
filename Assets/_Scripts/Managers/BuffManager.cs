using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public List<GameObject> Buffs;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var character = FindObjectOfType<BaseCharacter>();
            BuffEnum buffenum = Buffs[0].GetComponent<BuffBase>().Name;
            if (character.Buffs.ContainsKey(buffenum))
            {
                character.Buffs[buffenum].Upgrade();
            }
            else
            {
                var buff = PoolManager.Instance.SpawnBuff(BuffEnum.Buff1, character.transform);
                character.Buffs.Add(buffenum, buff.GetComponent<Ability>());
                buff.transform.localScale = Vector3.zero;
                buff.GetComponent<Ability>().Execute();
            }
        }
    }
}
