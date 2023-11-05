using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;
    //public GameObject[] events;
    public OneShotEvent[] oneShots;
    private float m_timer = 0.0f;
    public float minTimeBetweenEvents = 10.0f;
    public float maxTimeBetweenEvents = 30.0f;
    private OneShotEvent m_lastEvent = null;
    private void Awake()
    {
        current = this;
    }

    public 
    // Start is called before the first frame update
    void Start()
    {
        m_timer = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
        if(m_timer < 0)
        {
            m_timer = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
            OneShotEvent e = oneShots[Random.Range(0, oneShots.Length)];
            int i = 0;
            while(e == m_lastEvent && i++<20)
            {
                e = oneShots[Random.Range(0, oneShots.Length)];
            }
            if(i >= 20)
            {
                Debug.Log("Could not find a new event to fire!");
                return;
            }
            m_lastEvent = e;
            e.FireEvent();
            Statistics.EventsLived++;
        }

    }
}
