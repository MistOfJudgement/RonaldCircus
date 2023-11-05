using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEvent : OneShotEvent
{

    public GameObject bubble;
    // Start is called before the first frame update
    void Start()
    {
        FireEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void FireEvent()
    {
        AnnouncerBox.current.DisplayText("BUBBLE TIME!");
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach(EnemyController enemy in enemies)
        {
            GameObject b = Instantiate(bubble, enemy.transform.position, Quaternion.identity);
            enemy.transform.parent = b.transform;
            enemy.GetComponent<EnemyController>().Disable();
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
