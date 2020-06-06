using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] GameObject _spawnPoint, _theHQ, _enemiesContainer;
    [SerializeField] List<GameObject> enemies;
    public static List<GameObject> enemiesPool = new List<GameObject>();
    [SerializeField] bool _isRandomSpawning, _isSpawningRoutineRunning = false;
    [SerializeField] int _indexToSpawn, _enemyToSpawn;

    [Range(0.0f, 10.0f)] [SerializeField] float _spawnRate;

    public static Action OnWaveFinished;


    // Start is called before the first frame update
    void Awake()
    {
        

        if (_theHQ == null)
        {
            _theHQ = GameObject.Find("TheHQ");
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(nameof(Spawning));
        _isSpawningRoutineRunning = true;
        Debug.Log("Spawning start");
    }

    public void StopSpawning()
    {
        StopCoroutine(nameof(Spawning));
        OnWaveFinished?.Invoke();
        _isSpawningRoutineRunning = false;
        Debug.Log("Spawning stop");
    }

    public void AddToSpawn(int addSpawn)
    {
        _enemyToSpawn += addSpawn;
        if (!_isSpawningRoutineRunning)
        {
            SpawnManager.Instance.StartSpawning();
        }
    }

    IEnumerator Spawning()
    {
        int totalEnable = 0;

        while (_enemyToSpawn > 0)
        {
            if (PoolManager.enemiesSpawned != null)
            {
                foreach (var enemyObj in PoolManager.enemiesSpawned)
                {
                    if (enemyObj.activeInHierarchy == false)
                    {
                        enemyObj.SetActive(true);
                        enemyObj.GetComponent<NavMeshAgent>().Warp(_spawnPoint.transform.position);
                        enemyObj.GetComponent<NavMeshAgent>().SetDestination(_theHQ.transform.position);
                        Debug.Log("Reuse");
                        break;
                    }
                    else
                    {
                        totalEnable++;
                    }
                }
            }

            if (totalEnable == PoolManager.enemiesSpawned.Count)
            {
                if (_isRandomSpawning)
                {
                    _indexToSpawn = UnityEngine.Random.Range(0, enemies.Count);
                }

                GameObject enemy = Instantiate(enemies[_indexToSpawn]);
                enemy.GetComponent<NavMeshAgent>().Warp(_spawnPoint.transform.position);
                enemy.transform.parent = _enemiesContainer.transform;
                //enemy.GetComponent<Enemy1>()._target = _theHQ;
                PoolManager.Instance.AddEnemyToPool(enemy);

                Debug.Log("New");
            }
            totalEnable = 0;
            _enemyToSpawn--;
            yield return new WaitForSeconds(_spawnRate);
        }

        StopSpawning();
    }

}
