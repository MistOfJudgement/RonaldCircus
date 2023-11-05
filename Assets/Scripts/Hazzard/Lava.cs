using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour, IHitter<PlayerController>, IHitter<EnemyController>
{
    public int damage = 1;
    public void DoHit(PlayerController hittable)
    {
        hittable.TakeDamage(damage);
        hittable.OnHit();
    }

    public void DoHit(EnemyController hittable)
    {
        hittable.health.CurrentHealth -= damage;
        hittable.OnHit();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoHit(other.GetComponent<PlayerController>());
        }
        else if (other.CompareTag("Enemy"))
        {
            DoHit(other.GetComponent<EnemyController>());
        }
    }
}
