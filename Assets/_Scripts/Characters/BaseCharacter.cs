using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected CharacterStats Stats;

    public void InitCharacterStats(long health, long defense, long damage, long speed)
    {
        Stats = new CharacterStats(health, defense, damage, speed);
    }
}
