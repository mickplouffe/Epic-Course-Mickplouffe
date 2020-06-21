using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Need optimisation
/// </summary>
public class TurretTargeting : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 1, _range = 100, _viewAngle = 15;
    [SerializeField] GameObject _rotationObj, atkRangeSphere;
    public HitScanTurretBehaviour behaviourScript;
    [SerializeField] bool _isTargetLocked = false;
    GameObject _target;

    private void Start()
    {
        UpdateAtkRange();

        Invoke("OnSelected", 2);
    }

    void Update()
    {
        Aiming();
    }

    void UpdateAtkRange()
    {
        atkRangeSphere.transform.localScale = new Vector3(_range,_range,_range) / 4;
    }

    private void OnSelected() //I the turret is selected, Show range, Else do not.
    {
        atkRangeSphere.SetActive(false);
    } 

    private void Aiming()
    {
        _target = FindClosestEnemy();
        if (_target != null)
        {
            Vector3 targetDirection = _target.transform.position - _rotationObj.transform.position;
            float singleStep = _rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(_rotationObj.transform.forward, targetDirection, singleStep, 0.0f);

            _rotationObj.transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
        }

        if (FindVisibleTargets())
        {
            behaviourScript.Fire(_target);

        }
        else
        {
            behaviourScript.Fire();

        }

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
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

    bool FindVisibleTargets()
    {
        if (_target != null)
        {
            Vector3 dirToTarget = (new Vector3(_target.transform.position.x, 0, _target.transform.position.z) - new Vector3(_rotationObj.transform.position.x, 0, _rotationObj.transform.position.z)).normalized;
            if (Vector3.Angle(_rotationObj.transform.forward, dirToTarget) < _viewAngle / 2)
                return true;
            
        }
        return false;

    }


}
