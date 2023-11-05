using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TitleScene : MonoBehaviour
{
    [Serializable]
    public class Probability
    {
        public string value;
        public int probability;
    }
    public Probability[] objects;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int total = 0;
        foreach(Probability p in objects)
        {
            total += p.probability;
        }
        int random = Random.Range(0, total);
        int current = 0;
        foreach(Probability p in objects)
        {
            current += p.probability;
            if(random < current)
            {
                SetTitle(p.value);
                break;
            }
        }

    }
    public void SetTitle(string title)
    {
        GameObject.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = title;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        
        SceneManager.LoadScene("Battleground");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
