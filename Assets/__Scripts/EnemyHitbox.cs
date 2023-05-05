using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidible
{
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Enemy enemy = gameObject.GetComponent<Enemy>();
        if (player != null)
        {
            Damage dmg = new Damage
            {
                attackDmg = enemy.damage - player.armor
            };
            player.TakeDamage(dmg);
        }
    }
}

