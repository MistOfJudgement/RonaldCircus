using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IHitter<EnemyController>
{
    public float speed = 20f;
    public float damage = 10f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController hittable = collision.GetComponent<EnemyController>();
        if (hittable != null)
        {
            DoHit(hittable);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    public void DoHit(EnemyController hittable)
    {

        hittable.health.CurrentHealth -= (int)(damage);
        hittable.OnHit();
        Destroy(gameObject);

    }
}
