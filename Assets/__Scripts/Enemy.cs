using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    GameManager gameManager;
    protected RaycastHit hit;
    private Transform playerTransform;
    public int damage = 2;
    private int amount = 1;


    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log("get enemy box");
        
    }

    protected override void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        
        if (Physics.Raycast(transform.position, direction, out hit, 0.7f))
        {
            if (hit.collider.CompareTag("EnemyHitbox"))
            {
                // Выбираем случайное направление, если перед нами другой враг
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                direction = new Vector3(randomDirection.x, 0, randomDirection.y);
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            }
        }

        transform.position += Time.deltaTime * moveSpeed * direction;


        // Вычисляем поворот взгляда врага в направлении движения
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);
    }


    protected override void RecieveDamage(Damage dmg)
    {
        base.RecieveDamage(dmg);
    }

    public void TakeDamage(Damage damage)
    {
        RecieveDamage(damage);
    }


    

    protected override void Death()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddScore(amount);
        playerTransform.GetComponent<Player>().GetXp(xpValue);
        Debug.Log("Dead");
        Destroy(gameObject);
    }
}

