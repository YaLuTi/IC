using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor {

    private void OnSceneGUI()
    {
        FieldOfView fieldOfView = (FieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward, 360, fieldOfView.Radius);

        Vector3 viewAngleA = fieldOfView.DirFromAngle(-fieldOfView.Angle / 2, false);
        Vector3 viewAngleB = fieldOfView.DirFromAngle(+fieldOfView.Angle / 2, false);

        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + viewAngleA * fieldOfView.Radius);
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + viewAngleB * fieldOfView.Radius);
    }
}
