using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] TurretTargeting _turretTargeting;
    [SerializeField] int _warFundCost = 100;

    private void Shooting(GameObject target)
    {
        if (_turretTargeting.IsTargetLocked())
        {
            //Shoot
        }
    }


    public int GetWarFundCost()
    {
        return _warFundCost;
    }

}