using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.transform.position ;
    }
    private void LateUpdate()
    {
        Vector3 desirePos =target.transform.position + offset;
        transform.position = desirePos;
    }
}
