using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class OneShotEvent : GameEvent
{
    public abstract void FireEvent();
}
