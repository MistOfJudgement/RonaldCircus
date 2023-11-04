using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private void Start()
    {
        OnDeath += EnemyHealth_OnDeath;
    }

    private void EnemyHealth_OnDeath()
    {
        Destroy(gameObject);
    }
}
