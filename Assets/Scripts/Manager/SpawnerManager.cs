using System.Collections;
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
    [SerializeField] int _currentSubWave = 0, _subWaveInCurrentWave = 0;
    [SerializeField] string _currentSubWaveName = "";
    [SerializeField] List<GameObject> _enemies, _turrets;
    [SerializeField] bool _isRandomSpawn;

    [SerializeField] List<Wave> _waves;

    //--- IENumerator Init ---//
    Coroutine instSpawning = null;

    private void OnEnable() => GameManager.resetGameEvent += ResetSpawnerManager;
    private void OnDisable() => GameManager.resetGameEvent -= ResetSpawnerManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))        
            HUDController.Instance.WaveStartTimer(5);
        

        if (Input.GetKeyDown(KeyCode.R))        
            StopCoroutine(instSpawning);
        
    }

    public void SetEnemyList(List<GameObject> enemiesList) => _enemies = enemiesList;

    public List<Wave> WaveList { get => _waves; set => _waves = value; }

    void ResetSpawnerManager()
    {
        if (instSpawning != null)        
            StopSpawn();        
    }

    public int CurrentWave => _currentSubWave;

    public int SubWaveInCurrentWave => _subWaveInCurrentWave;

    public string CurrentWaveName => _currentSubWaveName;

    public GameObject SpawnTurret(string turretName)
    {
        GameObject foundTurretPooled = PoolManager.turretPooled.FirstOrDefault(j => j.name == turretName + "(Clone)" && j.activeSelf == false);
        if (foundTurretPooled == null)
        {
            GameObject turretObj = Instantiate(_turrets.Find(x => x.name == turretName));
            PoolManager.Instance.AddTurretToPool(turretObj);
            return turretObj;
        }
        else
        {
            return foundTurretPooled;
        }
    }

    public void SpawnEnemy(string enemyType = null)
    {
        GameObject foundEnemy = _enemies.FirstOrDefault(i => i.name == enemyType);
        GameObject enemyInst = foundEnemy != null ? Instantiate(foundEnemy) : Instantiate(_enemies[Random.Range(0, _enemies.Count)]);
        enemyInst.GetComponent<NavMeshAgent>().Warp(_spawnPoint.transform.position);
        enemyInst.transform.parent = _enemiesContainer.transform;
        PoolManager.Instance.AddEnemyToPool(enemyInst);
    }

    public void StartSpawning() => instSpawning = StartCoroutine(SpawningEnemy());

    void StopSpawn() => StopCoroutine(instSpawning);

    void ReuseEnemy(GameObject enemyToReuse = null)
    {
        if (enemyToReuse == null)        
            enemyToReuse = _enemies[Random.Range(0, _enemies.Count)];
        
        enemyToReuse.SetActive(true);

        NavMeshAgent navMeshAgentComp = enemyToReuse.GetComponent<NavMeshAgent>();
        navMeshAgentComp.Warp(_spawnPoint.transform.position);
        navMeshAgentComp.enabled = true;
        navMeshAgentComp.SetDestination(_theHQ.transform.position);
    }


    IEnumerator SpawningEnemy()
    {
        float SpawnDelay;
        float WaveDelay;

        for (int i = 0; i < _waves.Count; i++)
        {
            Wave wavesItem = _waves[i];
            _subWaveInCurrentWave = wavesItem.SubWaves.Count;
            _currentSubWave = 0;
            for (int i1 = 0; i1 < wavesItem.SubWaves.Count; i1++)
            {
                SubWave subWavesItem = wavesItem.SubWaves[i1];
                _currentSubWave++;
                _currentSubWaveName = subWavesItem.SubWaveName;

                HUDController.Instance.UpdateHUD("Waves");

                float waveDelay = subWavesItem.NextWaveDelay;

                if (waveDelay != 0)
                {
                    WaveDelay = waveDelay;
                }
                else
                {
                    WaveDelay = _defaultWaveDelay;
                }

                foreach (var (nameTypeName, spawnDelay, foundEnemyPooled) in from enemiesItem in subWavesItem.EnemyComposition
                                                                             let nameTypeName = enemiesItem.enemyTypeName
                                                                             let spawnDelay = enemiesItem.nextSpawnDelay
                                                                             let foundEnemyPooled = PoolManager.enemiesPooled.FirstOrDefault(j => j.name == nameTypeName + "(Clone)" && j.activeSelf == false)
                                                                             select (nameTypeName, spawnDelay, foundEnemyPooled))
                {
                    if (foundEnemyPooled == null)
                    {
                        SpawnEnemy(nameTypeName.ToString());
                    }
                    else
                    {
                        ReuseEnemy(foundEnemyPooled);
                    }

                    SpawnDelay = spawnDelay != 0 ? spawnDelay : _defaultSpawnDelay;

                    yield return new WaitForSeconds(SpawnDelay);
                }

                yield return new WaitForSeconds(WaveDelay);

            }
        }
        Debug.Log("Waves Finished");
        StopCoroutine(instSpawning);
    }


}
