using UnityEditor;
using UnityEngine;

public class UnityAnswers_CustomUnityEditorDuplicator : MonoBehaviour
{

    [MenuItem("Editor Extensions/Duplicate Selected and make it as a sibling #%d")]
    static void DoSomethingWithAShortcutKey()
    {
        GameObject[] objects = new GameObject[Selection.gameObjects.Length];
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            GameObject duped = Instantiate(Selection.gameObjects[i], Selection.gameObjects[i].transform.position, Selection.gameObjects[i].transform.rotation, Selection.gameObjects[i].transform.parent);
            duped.transform.SetSiblingIndex(Selection.gameObjects[i].transform.GetSiblingIndex() + 1);
            duped.name = Selection.gameObjects[i].name;
            Undo.RegisterCreatedObjectUndo(duped, "DoSomethingWithAShortcutKey");
        }
    }
}