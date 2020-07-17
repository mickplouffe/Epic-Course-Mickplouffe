using System;

public enum EnemyTypeName { Meka1, Meka2, Random };


[Serializable]
public class WaveEnemyComposition
{
    public EnemyTypeName enemyTypeName;
    public float nextSpawnDelay;
}
