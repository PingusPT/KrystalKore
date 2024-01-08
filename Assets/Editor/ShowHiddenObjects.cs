using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowHiddenObjects : EditorWindow
{
    [MenuItem("Tools/Show Hidden Objects")]
    public static void Init()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow ssWindow = EditorWindow.GetWindow(typeof(ShowHiddenObjects), false);
        //Show
        ssWindow.Show();

    }

    private GameObject[] objects;

    private void OnEnable()
    {
        Refresh();
    }
    private void OnGUI()
    {
        if (GUILayout.Button("REFRESH")) Refresh();

        EditorGUILayout.Space();

        foreach (GameObject item in objects)
        {
            if (item == null) continue;

            if (item.hideFlags != HideFlags.None)
            {
                EditorGUILayout.LabelField(item.name + " " + item.hideFlags.ToString());

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Select")) Selection.activeGameObject = item;
                    if (GUILayout.Button("Delete"))
                    {
                        DestroyImmediate(item);
                    }

                }
            }
        }
    }

    private void Refresh()
    {
        objects = FindObjectsOfType<GameObject>();
    }
}
