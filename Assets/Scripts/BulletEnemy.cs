using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<PlayerController>().HandleHit(this);
            Destroy(gameObject);
        }

    }
}
