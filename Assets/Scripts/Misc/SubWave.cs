using System;
using System.Collections;
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
    /// <summary>
    /// Delay in seconds to wait before launching the next Wave. If 0, use default delay.
    /// </summary>
    public float NextWaveDelay => _nextWaveDelay;

}
