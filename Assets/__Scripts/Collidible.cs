using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidible : MonoBehaviour
{
    public LayerMask collisionMask;
    private Collider[] hits = new Collider[20];

    protected Collider coll;

    protected virtual void Start()
    {
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionMask == (collisionMask | (1 << other.gameObject.layer)))
        {
            // ��������� ������������
            //Debug.Log("Object collided: " + other.gameObject.name);
        }
    }

    private void Update()
    {
        // ��������� ������������ ������ ����
        int numHits = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale / 2f, hits, transform.rotation, collisionMask);
        for (int i = 0; i < numHits; i++)
        {
            // ��������� ������������
            //Debug.Log("Object collided: " + hits[i].gameObject.name);
        }
    }
}

