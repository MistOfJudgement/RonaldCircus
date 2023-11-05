using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerHealth playerHealth;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public GameObject[] myyyyyy;
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void OnEnable()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }
        playerHealth.OnHealthChanged += UpdateHealthText;
        UpdateHealthText();

    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealthText;
    }

    public void UpdateHealthText()
    {
        healthText.text = "Health: " + playerHealth.CurrentHealth + "/" + playerHealth.maxHealth;
        for(int i = 0; i < myyyyyy.Length; i++)
        {
            myyyyyy[i].GetComponent<Image>().sprite = fullHeart;
            myyyyyy[i].SetActive(false);
        }



        for(int i = 0;i < playerHealth.CurrentHealth/2; i++)
        {
            myyyyyy[i].SetActive(true);
        }

        if(playerHealth.CurrentHealth%2 != 0)
        {
            myyyyyy[playerHealth.CurrentHealth / 2].SetActive(true);
            myyyyyy[playerHealth.CurrentHealth/2].GetComponent<Image>().sprite = halfHeart; 
        }

    }
}
