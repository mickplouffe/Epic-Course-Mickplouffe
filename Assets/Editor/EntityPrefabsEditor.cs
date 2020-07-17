using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EntityPrefabsEditor : EditorWindow
{
    [MenuItem("Dev Tools/Edit/Entities Prefabs")]
    public static void ShowWindow()
    {
        GetWindow<EntityPrefabsEditor>("Tool: Entity Prefabs Edition");
    }

    private void OnGUI()
    {
        GUILayout.Label("All Entity Prefabs", EditorStyles.boldLabel);



        if (GUILayout.Button("Test1"))
        {

        }

        if (GUILayout.Button("Edit selected"))
        {

        }


    }
}
