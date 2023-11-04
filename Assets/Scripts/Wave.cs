using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class Wave : ScriptableObject
{
    
    public Enemy[] enemies;

    public UnityEvent beforeWave;
    public UnityEvent onWaveEnd;

}



[Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public Vector2 spawnPosition;
}