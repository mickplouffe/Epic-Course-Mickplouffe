using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    public static List<GameObject> enemiesSpawned = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEnemyToPool(GameObject enemy)
    {
        enemiesSpawned.Add(enemy);

    }

    public void RemoveEnemyToPool(GameObject enemy)
    {
        enemiesSpawned.Add(enemy);

    }
}
