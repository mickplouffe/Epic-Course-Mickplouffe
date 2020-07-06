using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "GameSettings/LevelSettings", order = 1)]

public class LevelSettings : ScriptableObject
{
    public float defaultTimeScale, defaultCameraCenterFocusSpeed;
    public int defaultWarFund, endPointHealth;
    public List<GameObject> defaultEnemies;
    public List<Wave> DefaultWaves;
}
