using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        Aiming();       

    }

    private void Shooting(GameObject target)
    {
        if (true)
        {

        }
    }

    private void Aiming()
    {
        GameObject _target = FindClosestEnemy();
        if (_target != null)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = _target.transform.position - transform.position;
                       
            // The step size is equal to speed times frame time.
            float singleStep = _rotationSpeed * Time.deltaTime;
            
            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));

            Shooting(_target);

        }

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
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
