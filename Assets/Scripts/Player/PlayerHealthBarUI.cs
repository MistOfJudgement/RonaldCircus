using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerHealth playerHealth;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealthText;

    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealthText;
    }

    public void UpdateHealthText()
    {
        healthText.text = "Health: " + playerHealth.CurrentHealth + "/" + playerHealth.maxHealth;
    }
}
