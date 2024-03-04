using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public interface Ability
{
    /// <summary>
    /// Execute the skill or buff
    /// </summary>
    public void Execute();



    /// <summary>
    /// Returns the specific gameobject for each skill or buff
    /// </summary>
    /// <returns></returns>
    public GameObject ReturnGameObject();

    void Upgrade();
}
