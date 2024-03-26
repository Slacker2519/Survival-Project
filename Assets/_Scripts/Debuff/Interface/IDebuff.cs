using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebuff
{

    public void Execute(object data);

    public GameObject ReturnGameObject();

    void Upgrade();
}
