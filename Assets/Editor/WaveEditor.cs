using UnityEditor;
using UnityEngine;
using System.Linq;


public class WaveEditor : EditorWindow
{
    bool collapseWave, waveNameEmpty, waveNameExist, WaveCreatedConfirm, waveAlreadyExist;
    Vector2 scrollPosition;
    string newWaveName;
    Editor myWaveEditor;
    Wave[] myWaves;


    [MenuItem("Dev Tools/Edit/Waves")]
    public static void ShowWindow()
    {
        GetWindow<WaveEditor>("Tool: Wave Creator");
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
        scrollPosition = GUILayout.BeginScrollView(scrollPosition,false,false);

        collapseWave = EditorGUILayout.Foldout(collapseWave, "All Waves");
        myWaves = GetAllInstances<Wave>();

        if (collapseWave)
        {
            for (int i = 0; i < myWaves.Length; i++)
            {
                GUILayout.Label("Wave: " + myWaves[i].name, EditorStyles.boldLabel);
                myWaveEditor = Editor.CreateEditor(myWaves[i]);
                myWaveEditor.OnInspectorGUI();
                GUILayout.Space(10);
            }
        }

        GUILayout.Label("Create new Wave", EditorStyles.boldLabel);

        newWaveName = EditorGUILayout.TextField("Wave name:", newWaveName);
        if (GUILayout.Button("Create new Wave"))
        {

            if (AssetDatabase.LoadAssetAtPath("Assets/Waves/" + newWaveName + ".asset", typeof(Wave)) != null)            
                waveAlreadyExist = true;            
            else      
                waveAlreadyExist = false;            

            if (newWaveName == "" || newWaveName == null)
            {
                waveNameExist = false;
                WaveCreatedConfirm = false;
                waveNameEmpty = true;

            }
            else if (waveAlreadyExist) //Simply not working.
            {
                waveNameEmpty = false;
                WaveCreatedConfirm = false;
                waveNameExist = true;
            }
            else
            {
                waveNameExist = false;
                waveNameEmpty = false;
                WaveCreatedConfirm = true;
                Wave asset = ScriptableObject.CreateInstance<Wave>();
                AssetDatabase.CreateAsset(asset, "Assets/Waves/" + newWaveName + ".asset");
                //AssetDatabase.SaveAssets();
            }
        }

        if (waveNameEmpty)        
            GUILayout.Label("Cannot be empty.", EditorStyles.miniLabel);        
        if (waveNameExist)        
            GUILayout.Label("Wave already exist.", EditorStyles.miniLabel);        
        if (WaveCreatedConfirm)        
            GUILayout.Label("Wave created!", EditorStyles.miniLabel);

        

        GUILayout.EndScrollView();

    }
}
