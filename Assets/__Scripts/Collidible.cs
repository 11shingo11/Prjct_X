using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Collidible : MonoBehaviour
{
    public LayerMask collisionMask;
    private Collider[] hits = new Collider[20];
    private BoxCollider box;
    protected Collider coll;
    private bool colliding = false;
    public float raycastDistance = 1.0f;
    public float currMoveSpeed;
    public float pushPower = 2f;



    protected virtual void Start()
    {
        currMoveSpeed = GameManager.instance.player.moveSpeed;
        //Debug.Log(currMoveSpeed);
        coll = GetComponent<Collider>();        
        //Debug.Log(coll.ToString());
    }

    void Update()
    {
        
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        //{
        //    //GameManager.instance.player.transform.position -= transform.forward * 2f;
        //    //Debug.Log("Collision with " + hit.collider.gameObject.name);
        //}
        
    }

    void OnCollisionEnter(Collision collision)
    {

        
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}


