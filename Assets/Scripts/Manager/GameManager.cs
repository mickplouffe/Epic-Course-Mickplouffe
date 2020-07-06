using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("General Settings")]
    [SerializeField] LevelSettings currentLevelSettings;
    [SerializeField] bool _isSpawnerEnabled;
    [SerializeField] int warFund = 400, _health = 100;

    [Header("Time Scale Settings")]
    [SerializeField] float fixedFimestep = 0.02f;
    [SerializeField] [Range(0.0f, 10.0f)] float defaultTimeScale;
    [SerializeField] [Range(0.0f, 10.0f)] float currentTimeScale;


    public static Action resetGameEvent;

    private void OnEnable()
    {
        TheHQ.HQDamaged += ChangeHealth;
        defaultTimeScale = Time.timeScale;
        currentTimeScale = defaultTimeScale;
    }
    private void OnDisable()
    {
        TheHQ.HQDamaged -= ChangeHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_isSpawnerEnabled)
        {
            //SpawnManager.Instance.StartSpawning();

        }
        else
        {
            //SpawnManager.Instance.StopSpawning();

        }
    }

    public void AddWarFunds(int amountToChange)
    {
        warFund += amountToChange;

        if (warFund < 0)        
            warFund = 0;

        HUDController.Instance.UpdateWarFund();

    }

    public void SetWarFunds(int newAmount)
    {
        warFund = newAmount;

        if (warFund < 0)        
            warFund = 0;

        HUDController.Instance.UpdateWarFund();
    }

    public int GetWarFunds()
    {
        return warFund;
    }

    public float GetFixedTimestep()
    {
        return fixedFimestep;
    }

    public float GetTimeScale()
    {
        return defaultTimeScale;
    }

    public void ChangeTimeScale(float timeScaleMutiple)
    {
        if (timeScaleMutiple == 0)
        {
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            currentTimeScale = defaultTimeScale * timeScaleMutiple;
            if (Time.timeScale != 0)
            {
                Time.timeScale = currentTimeScale;

            }
        }
    }

    public void ChangeTimeScale()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = currentTimeScale;
        }
    }

    public void ChangeHealth(int HealthAmount = 1)
    {
        _health -= HealthAmount;
        if (_health < 0)
            _health = 0;
        HUDController.Instance.UpdateHUD("lives");
    }

    public void setHealth(int newHealth) => _health = newHealth;

    public int GetHealth()
    {
        return _health;
    }


    public void ResetGameSettings()
    {
        resetGameEvent?.Invoke();
        SetWarFunds(currentLevelSettings.defaultWarFund);
        ChangeHealth(currentLevelSettings.endPointHealth);
        GameManager.Instance.setHealth(currentLevelSettings.endPointHealth);

        ChangeTimeScale(currentLevelSettings.defaultTimeScale);
        SpawnerManager.Instance.SetEnemyList(currentLevelSettings.defaultEnemies);
        SpawnerManager.Instance.SetWaveList(currentLevelSettings.DefaultWaves);

        HUDController.Instance.UpdateHUD();

    }
}
