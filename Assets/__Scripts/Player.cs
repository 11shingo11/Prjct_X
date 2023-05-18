using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Mover
{
    public float playerDamage = 1f;
    public float currMana = 50f;
    public float maxMana = 50f;
    private FireMagic fireMagic;
    private bool isAlive = true;
    public float manaRecovery = 2.5f;
    private float recoverCooldown = 1.0f;
    private float lastRecover;
    public bool leveling = false;
    public GameObject fireballPrefab;
    public Damage dmg;
    public List<int> xpToLeveling;
    private int levelIndex = 0;

    protected override void Start()
    {
        base.Start();
        fireMagic = GetComponent<FireMagic>();
        //Debug.Log("get player box");

    }

    

    protected override void RecieveDamage(Damage dmg)
    {
        if (!isAlive)
            return;
        base.RecieveDamage(dmg);
    }

    public void TakeDamage(Damage damage)
    {
        RecieveDamage(damage);
    }

    protected override void Update()
    {
        base.Update();
        LvlUp();
        ManaRecovery();
        if (Input.GetKeyDown(KeyCode.Space))
            if (currMana - GameManager.instance.fire.manacost >= 0)
            {
                //anim.SetTrigger("attack");
                Shoot(fireballPrefab);
            }
            else
                return;

    }

    public void Shoot(GameObject pref)
    {
        GameObject fireball = Instantiate(pref, transform.position + transform.forward * 1.175f + transform.right * 0.544f + Vector3.up * 1.292f, transform.rotation);
        currMana -= GameManager.instance.fire.manacost;
        Destroy(fireball.gameObject, 5f);
    }
    public void GetXp(int xpValue)
    {
        xp += xpValue;
    }

    public void LvlUp()
    {

        if (xp >= xpToLeveling[levelIndex])
        {
            leveling = true;
            xp = 0;
            level += 1;
            hitpoint = maxHitpoint;
            currMana = maxMana;
            levelIndex++;
        }
        else
        {
            leveling = false; 
            return;
        }
           
    }

    private void ManaRecovery()
    {
        if (Time.time - lastRecover> recoverCooldown)
            if(currMana<maxMana)
            {
                lastRecover = Time.time;
                currMana += manaRecovery;
            }
    }

    protected override void Death()
    {
        isAlive = false;
        Destroy(gameObject);
        SceneManager.LoadScene("Main");
    }


    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("enter");
        
    }

    private void OnCollisionStay(Collision collision)
    {
        
        //Debug.Log("stay");
    }

    void FixedUpdate()
    {
        // Ограничение позиции игрока
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -64f, 64f),
            Mathf.Clamp(transform.position.y, -0f, 0f),
            Mathf.Clamp(transform.position.z, -79f, 75f)
        );
    }
}
