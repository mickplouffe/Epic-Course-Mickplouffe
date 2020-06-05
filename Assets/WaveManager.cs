using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    [SerializeField] int _currentWave, _amountToSpawn;

    public void StartWave()
    {
        
    }

    public void StoptWave()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextWave();
    }

    public void NextWave()
    {
        //AmountToSpawn = 10 * Wave

        _currentWave++;
        _amountToSpawn = 10 * _currentWave;
        SpawnManager.Instance.AddToSpawn(_amountToSpawn);
        SpawnManager.Instance.StartSpawning();
    }
}
