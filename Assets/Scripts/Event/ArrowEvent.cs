using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEvent : OneShotEvent
{
    public GameObject[] arrowPrefabs;
    public override void FireEvent()
    {
        GameObject prefab = arrowPrefabs[Random.Range(0, arrowPrefabs.Length)];
        Instantiate(prefab);
        AnnouncerBox.current.DisplayText("Get shot idiot! ;)");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
