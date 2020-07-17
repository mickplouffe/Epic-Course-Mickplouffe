using UnityEditor;
using UnityEngine;


public class SpawnAndPoolEditor : EditorWindow
{
    bool collapseSpawner, collapsePools;
    Vector2 scrollPosition;

    [MenuItem("Dev Tools/Edit/Spawn and Pools")]
    public static void ShowWindow()
    {
        GetWindow<SpawnAndPoolEditor>("Tool: Spawn and Pool");
    }

    private void OnGUI()
    {
        var redBoldStyle = new GUIStyle(GUI.skin.label);
        redBoldStyle.normal.textColor = Color.red;
        if (Application.isPlaying)
        {
            GUILayout.Box("Playing");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

            collapseSpawner = EditorGUILayout.Foldout(collapseSpawner, "Spawner Settings");
            if (collapseSpawner)
            {
                if (GUILayout.Button("Spawn One Random Enemy"))
                {
                    SpawnerManager.Instance.SpawnEnemy();
                }
            }

            collapsePools = EditorGUILayout.Foldout(collapsePools, "Pools Management");

            if (collapsePools && Application.isPlaying)
            {

                GUILayout.Label("Enemies Pooled", EditorStyles.boldLabel);
                foreach (var enemy in PoolManager.enemiesPooled)
                {
                    GUILayout.Label(enemy.name);
                }

                GUILayout.Label("Turrets Pooled", EditorStyles.boldLabel);
                foreach (var turret in PoolManager.turretPooled)
                {
                    GUILayout.Label(turret.name);
                }

            }


            GUILayout.EndScrollView();
        }
        else
        {
            GUILayout.Box("Not Playing", redBoldStyle);
        }

        
    }
}
