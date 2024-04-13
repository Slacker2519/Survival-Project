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
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnDebuff(FindObjectOfType<BaseCharacter>(),DeBuffEnum.BodyBurn);
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

    public void SpawnDebuff(BaseBody body, DeBuffEnum nameDebuff)
    {
        if (body)
        {
            var debuff = PoolManager.Instance.SpawnDeBuff(DeBuffEnum.BodyBurn, body.transform);
            DeBuffEnum debuffenum = debuff.GetComponent<DebuffBase>().Name;
            if (body.DeBuffs.ContainsKey(debuffenum))
            {
                body.DeBuffs[debuffenum].Upgrade();
                body.DeBuffs[debuffenum].Execute(null);
            }
            else
            {

                body.DeBuffs.Add(debuffenum, debuff.GetComponent<IDebuff>());
                debuff.transform.localScale = Vector3.zero;
                debuff.GetComponent<IDebuff>().Execute(null);
            }
        }
    }
}
