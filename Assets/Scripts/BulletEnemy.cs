using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour, IHitter<PlayerController>
{
    public float speed = 20f;
    public float damage = 10f;
    public float delay = 0f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("LUANCH", delay);
        
    }

    void LUANCH()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            DoHit(player);
        }

        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }

    public void DoHit(PlayerController hittable)
    {
        hittable.TakeDamage((int)(damage));
        hittable.OnHit();
        Destroy(gameObject);

    }

}
