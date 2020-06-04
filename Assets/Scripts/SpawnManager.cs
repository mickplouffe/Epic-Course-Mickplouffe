using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _spawnPoint;
    [SerializeField] List<GameObject> enemies;
    public static List<GameObject> enemiesSpawned = new List<GameObject>();
    public static List<GameObject> enemiesPool = new List<GameObject>();
    [SerializeField] bool _isRandomSpawning;
    [SerializeField] int _defaultIndexToSpawn;


    [Range(0.0f, 10.0f)] [SerializeField] float _spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(Spawning));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }


    IEnumerator Spawning()
    {
        while (true)
        {
            if (_isRandomSpawning)
            {
                _defaultIndexToSpawn = Random.Range(0, enemies.Count);
            }
            else
            {
                _defaultIndexToSpawn = 0;
            }
            enemiesSpawned.Add(Instantiate(enemies[_defaultIndexToSpawn], _spawnPoint.transform));
            yield return new WaitForSeconds(_spawnRate);

        }
    }

}
