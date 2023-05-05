using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Unit
{
    protected BoxCollider boxCollider;
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void followForMouse()
    {
        //Debug.Log("mouseWalk");
        anim.SetTrigger("walk");
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y - transform.position.y;
        Vector3 lookAtPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        lookAtPosition.y = transform.position.y;

        Vector3 direction = lookAtPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        float step = turningSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        float vertical = moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * vertical);
    }
    protected virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            followForMouse();
        }
        
        else
        {
            anim.SetTrigger("stand");
        }
        
    }
}
