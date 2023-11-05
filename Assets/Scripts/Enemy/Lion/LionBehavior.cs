using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionBehavior : MonoBehaviour, IEnemyBehavior, IHitter<PlayerController>
{
    private GameObject player;
    private Rigidbody2D rb;
    private enum State { Approaching, Pause, Dash, Cooldown}
    private State currentState = State.Approaching;
    public float speed = 3;
    public float acceleration = 0.3f;
    public float dashRadius = 2f;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(Approach());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Approach()
    {
        while(Vector2.Distance(transform.position, player.transform.position) > dashRadius)
        {
            Vector2 direction = player.transform.position - transform.position;
            rb.velocity += direction.normalized * acceleration * Time.deltaTime;
            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = rb.velocity.normalized * speed;
            }
            yield return null;
        }

        currentState = State.Pause;
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        float timer = 1f;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            rb.velocity -= rb.velocity.normalized * acceleration * Time.deltaTime;
            //prevent the lion from moving too close
            if(Vector2.Distance(transform.position, player.transform.position) < dashRadius/2)
            {
                rb.velocity -= (Vector2)((player.transform.position - transform.position) * acceleration*3 * Time.deltaTime);
            }
            yield return null;
        }
        currentState = State.Dash;
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed * 2;
        yield return new WaitForSeconds(0.5f);
        currentState = State.Cooldown;
        StartCoroutine(Cooldown());
    }
    
    IEnumerator Cooldown()
    {
        float timer = 3f;
        while(timer > 0 && rb.velocity.magnitude > 0.001f)
        {
            rb.velocity -= rb.velocity.normalized * acceleration * Time.deltaTime;
            timer -= Time.deltaTime;
            yield return null;
        }
        currentState = State.Approaching;
        StartCoroutine(Approach());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player != null)
        {
            DoHit(player);
        }
    }
    public void DoHit(PlayerController player)
    {
        player.TakeDamage(damage);
        player.OnHit();
    }

    public void StartBehavior()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        enabled = true;
        StartCoroutine(Approach());

    }

    public void StopBehavior()
    {
        currentState = State.Approaching;
        StopAllCoroutines();
        enabled = false;
    }
}
