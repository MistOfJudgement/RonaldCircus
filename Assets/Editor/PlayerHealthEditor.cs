using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerHealth))]
public class PlayerHealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        int val = EditorGUILayout.IntField("Current Health", ((PlayerHealth)target).CurrentHealth);
        if (EditorGUI.EndChangeCheck())
        {
            ((PlayerHealth)target).CurrentHealth = val;
        }

    }
}

