using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LionBehavior))]
public class LionEditor : Editor
{
    private void OnSceneGUI()
    {
        //draw circle for detection radius
        LionBehavior lion = (LionBehavior)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(lion.transform.position, Vector3.forward, Vector3.up, 360, lion.dashRadius);


    }
}
