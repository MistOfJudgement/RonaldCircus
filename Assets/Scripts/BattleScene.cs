using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.playerHealth.OnDeath += OnPlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayerDeath()
    {
        SceneManager.LoadScene("GameOver");
    }
}
