using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    public static List<GameObject> enemiesPooled = new List<GameObject>();

    public void AddEnemyToPool(GameObject enemy) => enemiesPooled.Add(enemy);

    public void RemoveEnemyToPool(GameObject enemy) => enemiesPooled.Add(enemy);

}
