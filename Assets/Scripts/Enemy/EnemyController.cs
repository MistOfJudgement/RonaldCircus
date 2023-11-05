using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(IEnemyBehavior))]
public class EnemyController : MonoBehaviour, IHittable
{
    public EnemyHealth health;
    public IEnemyBehavior behavior;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        //TODO Jank
        behavior = GetComponent<IEnemyBehavior>();
        behavior.StartBehavior();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Disable()
    {
        health.enabled = false;
        behavior.StopBehavior();
        this.enabled = false;
        rb.isKinematic = true;
    }
    public void Enable()
    {
        health.enabled = true;
        behavior.StartBehavior();
        this.enabled = true;
        rb.isKinematic = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit()
    {
        //noop
    }
}
