using System;
using UnityEngine;

public class TheHQ : MonoBehaviour
{
    public static Action<int> HQDamaged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemies otherEnemy = other.GetComponent<Enemies>();
            HQDamaged?.Invoke(otherEnemy.GetBodyDamage());
            otherEnemy.ResetEnemy();
        }
    }
}
