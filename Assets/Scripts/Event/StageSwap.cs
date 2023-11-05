using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSwap : OneShotEvent
{
    public GameObject[] arenas;
    public override void FireEvent()
    {
        //picks a random arena from the list of arenas
        int randomArena = Random.Range(0, arenas.Length);
        Destroy(Instantiate(arenas[randomArena]), 15f);
        AnnouncerBox.current.DisplayText("Can you handle a stage swap?!");
        
    }

}
