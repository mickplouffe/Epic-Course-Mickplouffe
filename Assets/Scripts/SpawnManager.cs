using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _spawnPoint;
    [SerializeField] List<GameObject> enemies;
    public static List<GameObject> enemiesSpawned = new List<GameObject>();
    public static List<GameObject> enemiesPool = new List<GameObject>();


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
            enemiesSpawned.Add(Instantiate(enemies[0], _spawnPoint.transform));
            yield return new WaitForSeconds(_spawnRate);

        }
    }

}
