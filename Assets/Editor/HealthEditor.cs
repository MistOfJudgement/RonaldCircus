using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health), true)]
public class HealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        int val = EditorGUILayout.IntField("Current Health", ((Health)target).CurrentHealth);
        if (EditorGUI.EndChangeCheck())
        {
            ((Health)target).CurrentHealth = val;
        }

    }
}

