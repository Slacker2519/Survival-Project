using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : BaseCharacter
{

    public CanShow ShowFunction;

    private void Awake()
    {
        _Rigidbody2d = GetComponent<Rigidbody2D>();
        ShowFunction.Show();
    }

    private void FixedUpdate()
    {
        this.PostEvent(EventID.OnPlayerMove, this.transform);
    }

    public override void InitCharacterStats(int level, CharacterEnum name, long health, long defense, long damage, long speed)
    {
        CharStats.Level = level;
        CharStats.Name = name;
        CharStats.Health = health;
        CharStats.Defense = defense;
        CharStats.Damage = damage;
        CharStats.Speed = speed;
    }
}

public interface CanShow
{
    void Show();
}

public class Show1 : MonoBehaviour, CanShow
{
    public void Show()
    {
        print("emdeplam");
    }
}
public class Show2 : MonoBehaviour, CanShow
{
    public void Show()
    {
        print("em xau vai");
        print("em xau vai");
    }
}

