using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour, IEnemyBehavior, IHitter<PlayerController>
{

    private GameObject player;
    public Rigidbody2D rb;
    private EnemyController storedEnemy;
    public float acceleration = 1f;
    public int damage = 1;

    private void Start()
    {
        storedEnemy = transform.childCount > 0 ? transform.GetChild(transform.childCount-1).GetComponent<EnemyController>() : null;
        storedEnemy?.Disable();
        storedEnemy?.transform.localPosition.Set(0, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        GetComponent<EnemyHealth>().OnDeath += Bubble_OnDeath;
    }

    private void Bubble_OnDeath()
    {
        if (storedEnemy == null)
        {
            return;
        }
        storedEnemy.Enable();
        storedEnemy.transform.parent = null;
        storedEnemy.transform.position = transform.position;
    }

    private void Update()
    {
        rb.velocity += 
            (Vector2)(player.transform.position - 
            transform.position).normalized * 
            acceleration * 
            Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var _player))
        {
            DoHit(_player);
        }
    }
    public void StartBehavior()
    {
        enabled = true;
    }

    public void StopBehavior()
    {
        enabled = false;
    }

    public void DoHit(PlayerController hittable)
    {
        hittable.TakeDamage(damage);
        hittable.OnHit();
    }

    public void TakeDamage(int damage)
    {
        GetComponent<EnemyHealth>().CurrentHealth -= (damage);
    }
}
