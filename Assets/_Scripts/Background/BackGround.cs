using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] Transform objectTrans;
    [SerializeField] Transform bgHolder;
    bool isDone = false;
    public void SetObjectTrans(Transform character)
    {
        objectTrans = character;
        bgHolder.transform.position = new Vector3(objectTrans.transform.position.x, objectTrans.transform.position.y, bgHolder.transform.position.z);
        isDone = true;
    }
    void UpdateTransX()
    {
        if (!isDone)
            return;
        //bgHolder.transform.position = new Vector3(bgHolder.transform.position.x, objectTrans.transform.position.y, bgHolder.transform.position.z);
        if (objectTrans.transform.position.x >= bgHolder.transform.position.x + 14)
            bgHolder.transform.position = new Vector3(objectTrans.transform.position.x + 14, bgHolder.transform.position.y, bgHolder.transform.position.z);
        else if (objectTrans.transform.position.x <= bgHolder.transform.position.x - 14)
            bgHolder.transform.position = new Vector3(objectTrans.transform.position.x - 14, bgHolder.transform.position.y, bgHolder.transform.position.z);
        
    }
    void UpdateY()
    {
        if (!isDone)
            return;
        if (objectTrans.transform.position.y >= bgHolder.transform.position.y + 21.5F/2)
            bgHolder.transform.position = new Vector3(bgHolder.transform.position.x, objectTrans.transform.position.y + 21.5F / 2, bgHolder.transform.position.z);
        else if (objectTrans.transform.position.y <= bgHolder.transform.position.y - 21.5F / 2)
            bgHolder.transform.position = new Vector3(bgHolder.transform.position.x, objectTrans.transform.position.y - 21.5F / 2, bgHolder.transform.position.z);
    }
    private void FixedUpdate()
    {
        UpdateTransX();


    }
    private void LateUpdate()
    {
        UpdateY();
    }
}
