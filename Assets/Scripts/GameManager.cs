using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] bool _isSpawnerEnabled;


    // Start is called before the first frame update
    void Start()
    {
        if (_isSpawnerEnabled)
        {
            SpawnManager.Instance.StartSpawning();

        }
        else
        {
            SpawnManager.Instance.StopSpawning();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
