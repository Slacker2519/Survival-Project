using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AutoDestroy : MonoBehaviour
{
    void Update()
    {
        if(Camera.main.WorldToViewportPoint(transform.position).x > 1 || 
            Camera.main.WorldToViewportPoint(transform.position).x < 0 ||
            Camera.main.WorldToViewportPoint(transform.position).y > 1 ||
            Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }
}
