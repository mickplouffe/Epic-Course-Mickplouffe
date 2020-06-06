using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    [SerializeField] int _currentWave, _amountToSpawn;

    private void OnEnable()
    {
        SpawnManager.OnWaveFinished += StartWave;
    }
    public void StartWave()
    {
        Invoke("NextWave", 20);
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
    }
}
