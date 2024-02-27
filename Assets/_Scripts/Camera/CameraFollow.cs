using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    void Start()
    {
        this.RegisterEvent(EventID.OnPlayerMove,OnMoveCamera);
    }

    void OnMoveCamera(object target)
    {
        Transform trans = target as Transform;
        if (trans != null)
        {
            transform.position = Vector3.Slerp(this.transform.position,new Vector3(trans.position.x,trans.position.y,transform.position.z),0.5f);

        }
    }
}
