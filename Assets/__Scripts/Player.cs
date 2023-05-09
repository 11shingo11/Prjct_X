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
    private float manaRecovery = 2.5f;
    private float recoverCooldown = 1.0f;
    private float lastRecover;
    public bool leveling = false;
    public GameObject fireballPrefab;
    public Damage dmg;

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
        Debug.Log("you recive "+dmg.attackDmg.ToString()+" damage");
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
            Shoot(fireballPrefab);
    }

    public void Shoot(GameObject pref)
    {
        GameObject fireball = Instantiate(pref, transform.position + transform.forward * 2.2f + transform.right * 1.2f + Vector3.up * 2f, transform.rotation);       
        Destroy(fireball.gameObject, 5f);
    }
    public void GetXp(int xpValue)
    {
        xp += xpValue;
    }

    public void LvlUp()
    {

        if (xp >= 5)
        {
            leveling = true;
            xp = 0;
            level += 1;
            hitpoint = maxHitpoint;
            moveSpeed += 0.1f;
            currMana = maxMana;
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

}
