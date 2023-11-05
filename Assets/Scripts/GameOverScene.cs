using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public TextMeshProUGUI waves;
    public TextMeshProUGUI enemies;
    public TextMeshProUGUI events;

    private void Start()
    {
        waves.text = string.Format(waves.text, Statistics.WavesSurvived);
        enemies.text = string.Format(enemies.text, Statistics.EnemiesKilled);
        events.text = string.Format(events.text, Statistics.EventsLived);
        Statistics.Reset();
    }
    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Battlefield");
    }
    
}
