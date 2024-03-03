using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : SkillBase
{
    public override void Execute()
    {
        Debug.Log("Demo execute skill 1");
    }

    public override GameObject ReturnGameObject()
    {
        return this.gameObject;
    }
}
