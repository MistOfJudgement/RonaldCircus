using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(IEnemyBehavior))]
public class EnemyController : MonoBehaviour
{
    public EnemyHealth health;
    public IEnemyBehavior behavior;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        behavior = GetComponent<IEnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
