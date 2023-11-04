using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    private Rigidbody2D rb;

    public float InvulnerabilityTime = 1f;
    private float invulnerabilityTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this; // this is the current instance of the script
        }
        else
        {
            //there can only be one
            Destroy(gameObject);
        }

        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb == null)
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HandleHit(collision.gameObject);
        }
    }
    private void HandleHit(GameObject enemy) {
        if(invulnerabilityTimer > 0)
        {
            return;
        }
        playerHealth.TakeDamage(5);
        invulnerabilityTimer = InvulnerabilityTime;
        StartCoroutine(InvulnerabilityFlash());

    }

    IEnumerator InvulnerabilityFlash()
    {
        while (invulnerabilityTimer > 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
