using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] bool _isSpawnerEnabled;
    [SerializeField] int warFund = 400;
    [SerializeField] float fixedFimestep = 0.02f;

    [Range(0.0f, 10.0f)]
    [SerializeField] float defaultTimeScale, currentTimeScale;

    public static Action<int> WarFundEvent;

    [SerializeField] float warFundAnimationTime = 1.5f;
    float desiredFunds, initialFunds, currentFunds;

    private void OnEnable()
    {
        WarFundEvent += ChangeWarFunds;
        defaultTimeScale = Time.timeScale;
        currentTimeScale = defaultTimeScale;
        currentFunds = warFund;
        desiredFunds = currentFunds;
        //https://www.youtube.com/watch?v=1r_BXVRjXvI
    }

    private void Update()
    {
        WarfundAnimation();
    }

    private void OnDisable() => WarFundEvent -= ChangeWarFunds;

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

    public void ChangeWarFunds(int amountToChange)
    {
        initialFunds = warFund;
        warFund += amountToChange;

        if (warFund < 0)
        {
            warFund = 0;
        }
        desiredFunds = warFund;
                
        HUDController.Instance.UpdateHUD("funds");
    }

    public int GetWarFunds()
    {
        return Mathf.RoundToInt(currentFunds);
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

    void WarfundAnimation()
    {
        if (currentFunds != desiredFunds)
        {
            if (initialFunds < desiredFunds)
            {
                currentFunds += (warFundAnimationTime * Time.fixedDeltaTime) * (desiredFunds - initialFunds);
                if (currentFunds >= desiredFunds)
                    currentFunds = desiredFunds;
            }
            else
            {
                currentFunds -= (warFundAnimationTime * Time.fixedDeltaTime) * (initialFunds - desiredFunds);
                if (currentFunds <= desiredFunds)
                    currentFunds = desiredFunds;
            }

            HUDController.Instance.UpdateHUD("funds");

        }
    }

}
