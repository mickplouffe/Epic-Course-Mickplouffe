using System.Linq;
using UnityEngine;

public class EnemyTurretTargeting : MonoBehaviour
{
    [SerializeField] GameObject _rotationObj, _target;
    [SerializeField] float _rotationSpeed = 1, _range = 30;

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
        foreach (var (go, curDistance) in from GameObject go in gos
                                          let diff = go.transform.position - position
                                          let curDistance = diff.sqrMagnitude
                                          where curDistance < distance
                                          select (go, curDistance))
        {
            closest = go;
            distance = curDistance;
        }

        return closest;
    }
}
