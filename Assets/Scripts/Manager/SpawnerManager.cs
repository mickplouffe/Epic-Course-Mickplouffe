﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// NEED REDO
/// </summary>
public class SpawnerManager : MonoSingleton<SpawnerManager>
{
    [SerializeField] GameObject _spawnPoint, _theHQ, _enemiesContainer;
    [SerializeField] float _defaultSpawnDelay = 1, _defaultWaveDelay = 10;
    [SerializeField] List<GameObject> _enemies;
    [SerializeField] bool _isRandomSpawn;

    [SerializeField] List<Wave> _waves;

    //--- IENumerator Init ---//
    Coroutine instSpawning = null;

    private void OnEnable() => GameManager.resetGameEvent += ResetSpawnerManager;
    private void OnDisable() => GameManager.resetGameEvent -= ResetSpawnerManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            instSpawning = StartCoroutine(Spawning());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StopCoroutine(instSpawning);
        }
    }

    public void SetEnemyList(List<GameObject> enemiesList)
    {
        _enemies = enemiesList;
    }

    public void SetWaveList(List<Wave> waveList)
    {
        _waves = waveList;
    }

    void ResetSpawnerManager()
    {
        if (instSpawning != null)
        {
            StopSpawn();

        }
    }

    void Spawn(string enemyType = null)
    {
        GameObject foundEnemy = _enemies.FirstOrDefault(i => i.name == enemyType);
        GameObject enemyInst;

        if (foundEnemy != null)
        {
            enemyInst = Instantiate(foundEnemy);
        }
        else
        {
            enemyInst = Instantiate(_enemies[Random.Range(0, _enemies.Count)]);

        }
        enemyInst.GetComponent<NavMeshAgent>().Warp(_spawnPoint.transform.position);
        enemyInst.transform.parent = _enemiesContainer.transform;
        PoolManager.Instance.AddEnemyToPool(enemyInst);
    }

    void StopSpawn() => StopCoroutine(instSpawning);
 

    void Reuse(GameObject enemyToReuse = null)
    {
        if (enemyToReuse == null)
        {
            enemyToReuse = _enemies[Random.Range(0, _enemies.Count)];
        }
        enemyToReuse.SetActive(true);

        NavMeshAgent navMeshAgentComp = enemyToReuse.GetComponent<NavMeshAgent>();
        navMeshAgentComp.Warp(_spawnPoint.transform.position);
        navMeshAgentComp.enabled = true;
        navMeshAgentComp.SetDestination(_theHQ.transform.position);
    }

    IEnumerator Spawning()
    {
        float SpawnDelay;
        float WaveDelay;

        foreach (var wavesItem in _waves)
        {
            foreach (var subWavesItem in wavesItem.SubWaves)
            {
                //Debug.Log(subWavesItem.SubWaveName);
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
