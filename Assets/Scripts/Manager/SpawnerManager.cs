using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class SpawnerManager : MonoSingleton<SpawnerManager>
{
    [SerializeField] GameObject _spawnPoint, _theHQ, _enemiesContainer;
    [SerializeField] float _defaultSpawnDelay = 1, _defaultWaveDelay = 10;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] bool _isRandomSpawn;

    [SerializeField] List<Wave> _waves; //If Next add Delay is == 0, use default. Else Add it to the default delay.

    //--- IENumerator Init ---//
    Coroutine instSpawning = null;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            instSpawning = StartCoroutine(Spawning(""));

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StopCoroutine(instSpawning);
        }
    }


    void Spawn(string enemyType = null)
    {
        GameObject foundEnemy = enemies.FirstOrDefault(i => i.name == enemyType);
        GameObject enemyInst;

        if (foundEnemy != null)
        {
            enemyInst = Instantiate(foundEnemy);
        }
        else
        {
            //Debug.LogError("The Enemy type ask does not exist! Spawning a random one instead.");
            enemyInst = Instantiate(enemies[Random.Range(0, enemies.Count)]);

        }
        enemyInst.GetComponent<NavMeshAgent>().Warp(_spawnPoint.transform.position);
        enemyInst.transform.parent = _enemiesContainer.transform;
        PoolManager.Instance.AddEnemyToPool(enemyInst);
    }

    void Reuse(GameObject enemyToReuse = null)
    {
        if (enemyToReuse == null)
        {
            enemyToReuse = enemies[Random.Range(0, enemies.Count)];
        }
        enemyToReuse.SetActive(true);
        enemyToReuse.GetComponent<NavMeshAgent>().Warp(_spawnPoint.transform.position);
        enemyToReuse.GetComponent<NavMeshAgent>().SetDestination(_theHQ.transform.position);
    }


    /// <summary>
    /// NEED CLEAN UP
    /// </summary>
    IEnumerator Spawning(string enemyType)
    {
        float SpawnDelay;
        float WaveDelay;

        foreach (var wavesItem in _waves)
        {
            foreach (var subWavesItem in wavesItem.SubWaves)
            {
                Debug.Log(subWavesItem.SubWaveName);
                float waveDelay = subWavesItem.NextWaveDelay;

                if (waveDelay != 0)
                {
                    WaveDelay = waveDelay;
                }
                else
                {
                    WaveDelay = _defaultWaveDelay;
                } 

                foreach (var enemiesItem in subWavesItem.EnemyComposition)
                {
                    string nameTypeName = enemiesItem.enemyTypeName;
                    float spawnDelay = enemiesItem.nextSpawnDelay;

                    GameObject foundEnemyPooled = PoolManager.enemiesPooled.FirstOrDefault(j => j.name == nameTypeName + "(Clone)" && j.activeSelf == false);

                    if (foundEnemyPooled != null)
                    {
                        Reuse(foundEnemyPooled);
                    }
                    else
                    {
                        Spawn(nameTypeName);
                    }

                    if (spawnDelay != 0)
                    {
                        SpawnDelay = spawnDelay;
                    }
                    else
                    {
                        SpawnDelay = _defaultSpawnDelay;
                    }


                    yield return new WaitForSeconds(SpawnDelay);
                }
                yield return new WaitForSeconds(WaveDelay);

            }
        }
        Debug.Log("Waves Finished");
        StopCoroutine(instSpawning);
    }

    
}
