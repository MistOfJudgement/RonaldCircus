using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics
{
    public static int WavesSurvived = 0;
    public static int EnemiesKilled = 0;
    public static int EventsLived = 0;
    public static void Reset()
    {
        WavesSurvived = 0;
        EnemiesKilled = 0;
        EventsLived = 0;
    }
}
