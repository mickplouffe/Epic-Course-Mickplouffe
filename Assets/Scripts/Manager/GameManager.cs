using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] bool _isSpawnerEnabled;
    [SerializeField] int warFund = 400;

    public static Action<int> WarFundEvent;

    private void OnEnable() => WarFundEvent += ChangeWarFunds;
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
        warFund += amountToChange;
        if (warFund < 0)
        {
            warFund = 0;
        }

        HUDController.Instance.UpdateHUD("funds");
    }

    public int GetWarFunds()
    {
        return warFund;
    }

}
