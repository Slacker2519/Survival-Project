using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BuffManager : MonoBehaviour
{
    //public List<GameObject> Buffs;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            SpawnBuffForCharacter();
        }
    }
    public void SpawnBuffForCharacter()
    {
        var character = FindObjectOfType<BaseCharacter>();

        if (character)
        {
            var buff = PoolManager.Instance.SpawnBuff(BuffEnum.Rocket, character.transform);
            BuffEnum buffenum = buff.GetComponent<BuffBase>().Name;
            if (character.Buffs.ContainsKey(buffenum))
            {
                character.Buffs[buffenum].Upgrade();
            }
            else
            {

                character.Buffs.Add(buffenum, buff.GetComponent<Ability>());
                buff.transform.localScale = Vector3.zero;
                buff.GetComponent<Ability>().Execute();
            }
        }
    }

    public void SpawnDebuffForCharacter(BaseCharacter character, DeBuffEnum nameDebuff)
    {
        if (character)
        {
            var debuff = PoolManager.Instance.SpawnDeBuff(DeBuffEnum.BodyBurn, character.transform);
            BuffEnum buffenum = debuff.GetComponent<BuffBase>().Name;
            if (character.Buffs.ContainsKey(buffenum))
            {
                character.Buffs[buffenum].Upgrade();
            }
            else
            {

                character.Buffs.Add(buffenum, debuff.GetComponent<Ability>());
                debuff.transform.localScale = Vector3.zero;
                debuff.GetComponent<Ability>().Execute();
            }
        }
    }
}
