using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;
    public Vector3 minLimit;
    public Vector3 maxLimit;


    private void Start()
    {
        offset = transform.position - target.transform.position ;
    }
    private void LateUpdate()
    {
        Vector3 desirePos =target.transform.position + offset;
        //transform.position = desirePos;
        transform.position = new Vector3(
            Mathf.Clamp(desirePos.x, minLimit.x, maxLimit.x),
            Mathf.Clamp(desirePos.y, minLimit.y, maxLimit.y),
            Mathf.Clamp(desirePos.z, minLimit.z, maxLimit.z)
        );
    }
}
