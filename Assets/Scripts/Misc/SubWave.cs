using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubWave
{
    [SerializeField] string _subWaveName;
    [SerializeField] List<WaveEnemyComposition> _enemyComposition;

    [SerializeField] float _nextWaveDelay;

    public string SubWaveName => _subWaveName;
    public List<WaveEnemyComposition> EnemyComposition => _enemyComposition;

    public float NextWaveDelay => _nextWaveDelay;

}
