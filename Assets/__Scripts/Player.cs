using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Mover
{
    public Fireball player;
    private bool isAlive = true;
    protected override void Start()
    {
        base.Start();
        
        Debug.Log("get player box");

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Shoot();
        }
    }

    public void GetXp(int xpValue)
    {
        xp += xpValue;
    }

    protected override void Death()
    {
        isAlive = false;
        Destroy(gameObject);
        SceneManager.LoadScene("Main");
    }

}
