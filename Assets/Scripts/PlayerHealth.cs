using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    //public int maxHealth = 100;
    //private int m_currentHealth;
    //public int CurrentHealth
    //{
    //    get
    //    {
    //        return m_currentHealth;
    //    }
    //    set
    //    {
    //        if (m_currentHealth != value)
    //        {
    //            m_currentHealth = value;
    //            OnHealthChanged?.Invoke();
    //        }
    //    }
    //}
    //public event Action OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        OnDeath += () => Debug.Log("Player died");
    }
}
