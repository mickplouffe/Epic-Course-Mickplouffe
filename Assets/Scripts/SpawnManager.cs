using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] GameObject _spawnPoint, _theHQ, _enemiesContainer;
    [SerializeField] List<GameObject> enemies;
    public static List<GameObject> enemiesPool = new List<GameObject>();
    [SerializeField] bool _isRandomSpawning;
    [SerializeField] int _indexToSpawn, _enemyToSpawn;


    [Range(0.0f, 10.0f)] [SerializeField] float _spawnRate;

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
    }

    public void StopSpawning()
    {
        StopCoroutine(nameof(Spawning));
        Debug.Log("Spawning stop");
    }

    public void AddToSpawn(int addSpawn)
    {
        _enemyToSpawn += addSpawn;
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
                    _indexToSpawn = Random.Range(0, enemies.Count);
                }
                GameObject enemy = Instantiate(enemies[_indexToSpawn]);
                enemy.GetComponent<NavMeshAgent>().Warp(_spawnPoint.transform.position);
                enemy.transform.parent = _enemiesContainer.transform;
                //enemy.GetComponent<Enemy1>()._target = _theHQ;
                PoolManager.Instance.AddEnemyToPool(enemy);
            }

            totalEnable = 0;

            _enemyToSpawn--;
            yield return new WaitForSeconds(_spawnRate);


        }

        StopSpawning();
    }

}
