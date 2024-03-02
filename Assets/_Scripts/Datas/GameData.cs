using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // Data need to save
    public CharacterData CharacterPicked;

    public GameData(DataAssets dataAssets)
    {
        var stat = dataAssets.GetCharacterConfig(CharacterEnum.Character1);
        CharacterPicked = new CharacterData(stat.Level, stat.Name, stat.Health, stat.Defense, stat.Damage, stat.Speed);
    }
}
