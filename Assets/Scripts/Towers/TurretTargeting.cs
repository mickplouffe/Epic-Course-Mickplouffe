using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 1, _range = 100;
    [SerializeField] GameObject atkRangeSphere;

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
        GameObject _target = FindClosestEnemy();
        if (_target != null)
        {
            Vector3 targetDirection = _target.transform.position - transform.position;
            float singleStep = _rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            //Debug.DrawRay(transform.position, newDirection, Color.red);

            transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
        }

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = _range;
        Vector3 position = transform.position;
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
