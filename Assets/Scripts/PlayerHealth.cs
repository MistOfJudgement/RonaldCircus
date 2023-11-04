using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int m_currentHealth;
    public int CurrentHealth
    {
        get
        {
            return m_currentHealth;
        }
        set
        {
            if (m_currentHealth != value)
            {
                m_currentHealth = value;
                OnHealthChanged?.Invoke();
            }
        }
    }
    public event Action OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Debug.Log("Player died!");
        }
        OnHealthChanged?.Invoke();
    }

    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke();
    }
}
