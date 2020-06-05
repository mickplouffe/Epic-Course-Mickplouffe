using UnityEngine;

/// <summary>
/// Mono singleton Class. Extend this class to make singleton component.
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;

                // Object not found, creating a temporary one
                if (_instance == null)
                {
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");

                    isTemporaryInstance = true;
                    _instance = new GameObject("Temporary Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    // Problem during the creation, this should not happen
                    if (_instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }
                if (!_isInitialized)
                {
                    _isInitialized = true;
                    _instance.Init();
                }
            }
            return _instance;
        }
    }

    public static bool isTemporaryInstance { private set; get; }

    private static bool _isInitialized;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
            DestroyImmediate(this);
            return;
        }
        if (!_isInitialized)
        {
            DontDestroyOnLoad(gameObject);
            _isInitialized = true;
            _instance.Init();
        }
    }


    public virtual void Init() { }

    private void OnApplicationQuit()
    {
        _instance = null;
    }
}