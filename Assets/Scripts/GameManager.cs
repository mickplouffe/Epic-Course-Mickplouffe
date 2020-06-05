using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] bool _isSpawnerEnabled;


    // Start is called before the first frame update
    void Start()
    {
        if (_isSpawnerEnabled)
        {
            SpawnManager.Instance.StartSpawning();

        }
        else
        {
            SpawnManager.Instance.StopSpawning();

        }
    }

}
