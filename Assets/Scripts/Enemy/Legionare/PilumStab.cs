using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilumStab : MonoBehaviour, IHitter<PlayerController>
{
    public int damage = 5;
    public void DoHit(PlayerController hittable)
    {
        hittable.TakeDamage(damage);
        hittable.OnHit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player != null)
        {
            DoHit(player);
        }
    }
}
