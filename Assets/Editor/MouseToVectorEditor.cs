using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonsterBasic), true)]
public class MouseToVectorEditor : Editor {

    Vector3 mousePosition;
    void OnSceneGUI()
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            mousePosition = hit.point;
        }
        
        Repaint();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        base.OnInspectorGUI();
        EditorGUILayout.Vector3Field("MousePointPosition", mousePosition);
    }
    
}
