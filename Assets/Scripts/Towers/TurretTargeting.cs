using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Need optimisation
/// </summary>
public class TurretTargeting : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 1, _range = 100, _viewAngle = 15, _fireRateDelay = 1;
    [SerializeField] int _damageDealing = 1;
    [SerializeField] GameObject atkRangeSphere;
    [SerializeField] bool _isTargetLocked = false, _fireRateCooldown;
    GameObject _target;

    private void OnDisable()
    {
        StopCoroutine(nameof(FiringCooldown));
    }

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

    public bool IsTargetLocked()
    {

        return _isTargetLocked;
    }

    bool FindVisibleTargets()
    {
        if (_target != null)
        {
            Vector3 dirToTarget = (new Vector3(_target.transform.position.x, 0, _target.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
            {
                Debug.LogError("TARGET FOUND!!!");
                float dstToTarget = Vector3.Distance(transform.position, _target.transform.position);
                return true;

            }
        }
        return false;

    }

    private void Aiming()
    {
        _target = FindClosestEnemy();
        if (_target != null)
        {
            Vector3 targetDirection = _target.transform.position - transform.position;
            float singleStep = _rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            //Debug.DrawRay(transform.position, newDirection, Color.red);

            transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
        }

        if (FindVisibleTargets())
        {

            if (!_fireRateCooldown)
            {
                StartCoroutine(nameof(FiringCooldown));
                _target.GetComponent<Enemies>().TakeDamage(_damageDealing);

            }

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

    IEnumerator FiringCooldown()
    {
            _fireRateCooldown = true;
            yield return new WaitForSeconds(_fireRateDelay);
            _fireRateCooldown = false;
        StopCoroutine(nameof(FiringCooldown));

    }
}
