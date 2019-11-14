using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEditor;

public class Test : MonoBehaviour
{

    [MenuItem("Favourites/Trees")]
    private static void SelectFolder()
    {
        /*string path = "Assets";

        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        Debug.Log(path);
        EditorUtility.FocusProjectWindow();*/
        Debug.Log(Directory.temporaryFolder);
        /*Object obj = AssetDatabase.LoadAssetAtPath<Object>("Assets/Scripts/Player/Okay");

        Selection.activeObject = obj;*/
    }
}
