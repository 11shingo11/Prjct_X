using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Collidible
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 100f;
    public int fireDamage = 3;
    public float explosionRadius = 8f;
    public GameObject explosionPrefab;


    protected override void Start()
    {
        base.Start();     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitbox") && coll.CompareTag("bullet"))
        {
            coll = other.GetComponentInChildren<BoxCollider>();
            Enemy e = coll.GetComponentInParent<Enemy>();
            e.TakeDamage(GetAttackValue());
            Explode(coll);
            Debug.Log("+++");
        }
    }
    public void Explode(Collider _1)
    {
        if (explosionPrefab != null)
        {
            ExplodeAnim();
        }

        transform.GetChild(0).gameObject.SetActive(true);

        Collider[] hits = new Collider[20]; // создаем массив коллайдеров размером 10 (можно увеличить, если необходимо)
        int numHits = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, hits);
        for (int i = 0; i < numHits; i++)
        {
            Collider hit = hits[i];
            //Debug.Log("getting explode foreach1");
            if (hit.CompareTag("Enemy"))
            {
                //Debug.Log("getting explode if1");
                if (hit.TryGetComponent(out Enemy e))
                {
                    //Debug.Log("getting explode if2");
                    e.TakeDamage(GetExplodeValue());
                }
            }
        }
        

        // ”ничтожаем снар€д
        Destroy(gameObject);
        
    }



    public void Shoot()
    {
        GameObject fireball = Instantiate(fireballPrefab, transform.position + transform.forward * 2.2f + transform.right * 1.2f + Vector3.up * 2f, transform.rotation);
        Rigidbody fireballRigidbody = fireball.GetComponent<Rigidbody>();
        fireballRigidbody.AddForce(transform.forward * fireballSpeed, ForceMode.Impulse);
        Destroy(fireball, 5f);
    }

    public Damage GetAttackValue()
    {
        Damage dmg = new Damage() { attackDmg = fireDamage };
        return dmg;
    }

    public Damage GetExplodeValue()
    {
        int expldmg = Mathf.FloorToInt(fireDamage * 25 / 100);
        Damage dmg = new Damage() { attackDmg = expldmg };
        return dmg;
    }

    private void ExplodeAnim()
    {
        GameObject explodeGO = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explodeGO, 2f);
    }
}

