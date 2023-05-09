using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float hitpoint = 0;
    public int maxHitpoint = 0;
    public int level = 0;
    public int armor = 0;
    public int xp = 0;
    public float moveSpeed = 0f;
    public float turningSpeed = 600f;
    public int xpValue = 0;


    protected float immuneTime = 0.2f;
    protected float lastImmune = -Mathf.Infinity;
    

    protected virtual void RecieveDamage(Damage dmg)
    {
        if (Time.time - lastImmune >= immuneTime)
        {
            lastImmune = Time.time;
            if (dmg.attackDmg >= 0)
                hitpoint -= dmg.attackDmg ;
            else
            {
                dmg.attackDmg = 0;
                hitpoint -= dmg.attackDmg;
            }
                



            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }
    protected virtual void Death()
    {
        
    }

}
