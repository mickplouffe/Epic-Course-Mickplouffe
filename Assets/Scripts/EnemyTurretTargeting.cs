﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretTargeting : MonoBehaviour
{
    [SerializeField] GameObject _rotationObj, _target;
    [SerializeField] float _rotationSpeed = 1, _range = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aiming();
    }


    private void Aiming()
    {
        _target = FindClosestTurret();

        if (_target != null)
        {
            Vector3 targetDirection = _target.transform.position - _rotationObj.transform.position;
            float singleStep = _rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(_rotationObj.transform.forward, targetDirection, singleStep, 0.0f);

            _rotationObj.transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, newDirection.y, newDirection.z));

        }

    }

    public GameObject FindClosestTurret()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Turret");
        GameObject closest = null;
        float distance = _range;
        Vector3 position = _rotationObj.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
