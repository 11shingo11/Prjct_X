using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireMagic : Collidible
{
    public float fireballDamage = 5f;
    public float explosionRadius = 5f;
    public float manacost = 5f;
    public float fireballSpeed = 100f;
    public float percentOfExpload = 15f;
    public GameObject explosionPrefab;


    protected override void Start()
    {
        base.Start();
        Launch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitbox") && coll.CompareTag("bullet"))
            {
            coll = other.GetComponentInChildren<BoxCollider>();
            Enemy e = coll.GetComponentInParent<Enemy>();
            e.TakeDamage(GetAttackValue());
            
            if (GameManager.instance.lvlmenu.explTru)
            {
                Explode(e);                
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Explode(Enemy e)
    {
        if (explosionPrefab != null)
        {
            ExplodeAnim();
        }
        transform.GetChild(0).gameObject.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider.ToString());
            if (collider.CompareTag("Enemy"))
            {
                Debug.Log(collider.ToString());
                collider.GetComponent<Enemy>().TakeDamage(GetExplodeValue());
                
            }
            
        }

        Destroy(gameObject);
    }
    public Damage GetAttackValue()
    {
        Damage dmg = new Damage() { attackDmg = fireballDamage + GameManager.instance.player.playerDamage };
        return dmg;
    }

    public Damage GetExplodeValue()
    {
        int expldmg = Mathf.FloorToInt(fireballDamage * percentOfExpload / 100);
        Damage dmg = new Damage() { attackDmg = expldmg };
        return dmg;
    }


   public void Launch()
    {
        Rigidbody fireballRigidbody = GetComponent<Rigidbody>();
        fireballRigidbody.AddForce(transform.forward * fireballSpeed, ForceMode.Impulse);
    }

    private void ExplodeAnim()
    {
        GameObject explodeGO = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explodeGO, 2f);
    }
}
