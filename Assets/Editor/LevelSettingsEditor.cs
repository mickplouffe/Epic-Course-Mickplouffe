using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class LevelSettingsEditor : EditorWindow
{
    bool collapseLevelSettings, levelSettingsNameEmpty, levelSettingsNameExist, levelSettingsCreatedConfirm, levelSettingsAlreadyExist;
    Vector2 scrollPosition;
    string newLevelSettingsName;
    Editor myLevelSettingsEditor;
    LevelSettings[] myLevelSettingss;

    LevelSettings[] myLevelSettings;

    [MenuItem("Dev Tools/Edit/Level Settings")]
    public static void ShowWindow()
    {
        GetWindow<LevelSettingsEditor>("Tool: Level Settings");
    }
    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }
        return a;
    }


    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

        collapseLevelSettings = EditorGUILayout.Foldout(collapseLevelSettings, "All Level Settings");
        myLevelSettings = GetAllInstances<LevelSettings>();

        if (collapseLevelSettings)
        {
            for (int i = 0; i < myLevelSettings.Length; i++)
            {
                GUILayout.Label("Level Settings: " + myLevelSettings[i].name, EditorStyles.boldLabel);
                myLevelSettingsEditor = Editor.CreateEditor(myLevelSettings[i]);
                myLevelSettingsEditor.OnInspectorGUI();
                GUILayout.Space(10);
            }
        }

        GUILayout.Label("Create new LevelSettings", EditorStyles.boldLabel);

        newLevelSettingsName = EditorGUILayout.TextField("Level Settings name:", newLevelSettingsName);
        if (GUILayout.Button("Create new Level Settings"))
        {

            if (AssetDatabase.LoadAssetAtPath("Assets/LevelSettings/" + newLevelSettingsName + ".asset", typeof(LevelSettings)) != null)
                levelSettingsAlreadyExist = true;
            else
                levelSettingsAlreadyExist = false;

            if (newLevelSettingsName == "" || newLevelSettingsName == null)
            {
                levelSettingsNameExist = false;
                levelSettingsCreatedConfirm = false;
                levelSettingsNameEmpty = true;

            }
            else if (levelSettingsAlreadyExist) //Simply not working.
            {
                levelSettingsNameEmpty = false;
                levelSettingsCreatedConfirm = false;
                levelSettingsNameExist = true;
            }
            else
            {
                levelSettingsNameExist = false;
                levelSettingsNameEmpty = false;
                levelSettingsCreatedConfirm = true;
                LevelSettings asset = ScriptableObject.CreateInstance<LevelSettings>();
                AssetDatabase.CreateAsset(asset, "Assets/LevelSettings/" + newLevelSettingsName + ".asset");
                //AssetDatabase.SaveAssets();
            }
        }

        if (levelSettingsNameEmpty)
            GUILayout.Label("Cannot be empty.", EditorStyles.miniLabel);
        if (levelSettingsNameExist)
            GUILayout.Label("Level Settings already exist.", EditorStyles.miniLabel);
        if (levelSettingsCreatedConfirm)
            GUILayout.Label("Level Settings created!", EditorStyles.miniLabel);



        GUILayout.EndScrollView();

    }
}
