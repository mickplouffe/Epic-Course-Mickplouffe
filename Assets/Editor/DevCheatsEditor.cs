using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DevCheatsEditor : EditorWindow
{
    Vector2 scrollPosition;
    float currentTimeScale;
    bool isUnilimitedHealth, isUnlimitedWarFunds;  //Currently not working well.
    int customHealth, customWarFunds;
    GameObject MaxTurretPrefab;

    [MenuItem("Dev Tools/Development Cheats", false, 0)]
    public static void ShowWindow()
    {
        GetWindow<DevCheatsEditor>("Development Cheats");
    }

    private void OnEnable() => GameManager.doRepaint += RepaintDevCheats;
    private void OnDisable() => GameManager.doRepaint -= RepaintDevCheats;


    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

        MaxTurretPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Turrets/Dual_Gatling_Gun.prefab", typeof(GameObject));

        var redBoldStyle = new GUIStyle(GUI.skin.label);
        redBoldStyle.normal.textColor = Color.red;

        if (EditorApplication.isPlaying)
        {
            GUILayout.Box("Playing");

            GUILayout.Label("General Cheats", EditorStyles.boldLabel);

            GUILayout.Label("Health Cheats", EditorStyles.miniBoldLabel);

            if (GUILayout.Button("Set to 100"))
            {
                GameManager.Instance.Health = 99;
                GameManager.Instance.ChangeHealth(-1);
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add 5"))
                GameManager.Instance.ChangeHealth(-5);
            if (GUILayout.Button("Add 1"))
                GameManager.Instance.ChangeHealth(-1);
            if (GUILayout.Button("Set to 0"))
            {
                GameManager.Instance.Health = 1;
                GameManager.Instance.ChangeHealth(1);
            }
            if (GUILayout.Button("Remove 1"))
                GameManager.Instance.ChangeHealth(1);
            if (GUILayout.Button("Remove 5"))
                GameManager.Instance.ChangeHealth(5);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            customHealth = EditorGUILayout.IntField("Custom health:", customHealth);
            isUnilimitedHealth = EditorGUILayout.Toggle("Unlimited Health", isUnilimitedHealth);
            EditorGUILayout.EndHorizontal();

            if (isUnilimitedHealth)
            {
                GameManager.Instance.Health = 100;
            }

            if (GUILayout.Button("Set"))
            {
                GameManager.Instance.Health = customHealth + 1;
                GameManager.Instance.ChangeHealth(1);
            }


            GUILayout.Space(10);

            GUILayout.Label("War Funds Cheats", EditorStyles.miniBoldLabel);

            if (GUILayout.Button("Set to 25k"))
                GameManager.Instance.WarFunds = 25000;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add 500"))
                GameManager.Instance.AddWarFunds(500);
            if (GUILayout.Button("Add 100"))
                GameManager.Instance.AddWarFunds(100);
            if (GUILayout.Button("Set to 0"))
                GameManager.Instance.WarFunds = 0;
            if (GUILayout.Button("Remove 100"))
                GameManager.Instance.AddWarFunds(-100);
            if (GUILayout.Button("Remove 500"))
                GameManager.Instance.AddWarFunds(-500);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            customWarFunds = EditorGUILayout.IntField("Custom War Funds: ", customWarFunds);
            isUnlimitedWarFunds = EditorGUILayout.Toggle("Unlimited Health", isUnlimitedWarFunds);
            EditorGUILayout.EndHorizontal();

            if (isUnlimitedWarFunds)
            {
                GameManager.Instance.WarFunds = 2000;
            }

            if (GUILayout.Button("Set"))
            {
                GameManager.Instance.WarFunds = customWarFunds;
            }
            GUILayout.Space(10);

            GUILayout.Label("Speed Hack", EditorStyles.boldLabel);

            Time.timeScale = EditorGUILayout.Slider(Time.timeScale, 0, 10);
            


            GUILayout.Space(10);

            GUILayout.Label("'Fun' Cheats", EditorStyles.boldLabel);

            if (GUILayout.Button("Super Mode"))
            {
                GameManager.Instance.Health = 696970;
                GameManager.Instance.ChangeHealth(1);

                GameManager.Instance.WarFunds = 8069;
                HUDController.UIColoration(Color.clear + (Color.white / 2));

                foreach (GameObject go in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    if (go.name == "Spot")
                    {
                        go.GetComponent<TurretSpot>().PlaceTower(MaxTurretPrefab);

                    }
                }
            }
        }
        else
        {
            GUILayout.Box("Not Playing", redBoldStyle);
        }


        GUILayout.EndScrollView();        
    }

    public void RepaintDevCheats()
    {
        Repaint();
    }
}
