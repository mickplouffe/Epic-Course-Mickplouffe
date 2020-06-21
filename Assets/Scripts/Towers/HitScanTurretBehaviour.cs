using System.Collections;
using UnityEngine;
using GameDevHQ.FileBase.Gatling_Gun;

public class HitScanTurretBehaviour : MonoBehaviour
{
    [SerializeField] float _fireRateDelay = 1;
    [SerializeField] Gatling_Gun gatlingScript;
    bool _fireRateCooldown;
    [SerializeField] int _damageDealing = 1;


    private void OnDisable()
    {
        StopCoroutine(nameof(FiringCooldown));
    }

    public void Fire(GameObject target = null)
    {
        if (target != null)
        {
            gatlingScript.SHOOT(true);

            if (!_fireRateCooldown)
            {
                StartCoroutine(nameof(FiringCooldown));
                target.GetComponent<Enemies>().TakeDamage(_damageDealing);

            }

        }
        else
        {
            gatlingScript.SHOOT(false);

        }
        
    }

    IEnumerator FiringCooldown()
    {
        _fireRateCooldown = true;
        yield return new WaitForSeconds(_fireRateDelay);
        _fireRateCooldown = false;
        StopCoroutine(nameof(FiringCooldown));

    }
}
