using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveDefinition", menuName = "Waves/WavesDefinition", order = 1)]
public class Wave : ScriptableObject
{
    [SerializeField] float _firstWaveDelay;

    [SerializeField] List<SubWave> _subWaves;

    public List<SubWave> SubWaves => _subWaves;

}
