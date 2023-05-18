using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mover : Unit
{
    protected BoxCollider boxCollider;
    protected Animator anim;
    protected Collider coll;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();      
    }

    private void FollowForMouse()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0f || vertical != 0f)
        {
            transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime);
            anim.SetTrigger("walk");
        }
        else
            anim.SetTrigger("stand");
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Quaternion targetRotation = Quaternion.LookRotation(point - transform.position);
            float step = turningSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        }
        

        ////Debug.Log("mouseWalk");
        //anim.SetTrigger("walk");
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = Camera.main.transform.position.y - transform.position.y;
        //Vector3 lookAtPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //lookAtPosition.y = transform.position.y;

        //Vector3 direction = lookAtPosition - transform.position;
        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        //float step = turningSpeed * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        //float vertical = moveSpeed * Time.deltaTime;
        //transform.Translate(Vector3.forward * vertical);
    }
    protected virtual void Update()
    {

        
        FollowForMouse();
        
        
        //else
        //{
        //anim.SetTrigger("stand");
        //}
        
    }
}
