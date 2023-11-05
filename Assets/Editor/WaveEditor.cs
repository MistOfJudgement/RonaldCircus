using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Wave))]
public class WaveEditor : Editor
{
    private void OnSceneGUI(SceneView sceneView)
    {
        Wave wave = (Wave)target;
        Handles.color = Color.red;
        if (wave.enemies == null) return;   
        foreach (Enemy enemy in wave.enemies)
        {
            Handles.DrawWireDisc(enemy.spawnPosition, Vector3.forward, 0.5f);
        }
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}
