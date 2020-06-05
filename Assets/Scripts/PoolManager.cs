using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    public static List<GameObject> enemiesSpawned = new List<GameObject>();

    public void AddEnemyToPool(GameObject enemy)
    {
        enemiesSpawned.Add(enemy);

    }

    public void RemoveEnemyToPool(GameObject enemy)
    {
        enemiesSpawned.Add(enemy);

    }
}
