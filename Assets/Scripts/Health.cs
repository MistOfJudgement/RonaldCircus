

using System;
using UnityEngine;

public abstract class Health: MonoBehaviour
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
            if(!enabled) return;
            if (m_currentHealth != value)
            {
                m_currentHealth = value;

                OnHealthChanged?.Invoke();

                if (m_currentHealth <= 0)
                {
                    OnDeath?.Invoke();
                }
            }
        }
    }
    public event Action OnHealthChanged;
    public event Action OnDeath;
    private void Awake()
    {
        CurrentHealth = maxHealth;
    }
}

