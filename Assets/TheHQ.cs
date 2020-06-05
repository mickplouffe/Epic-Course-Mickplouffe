using System;
using UnityEngine;

public class TheHQ : MonoBehaviour
{
    [SerializeField] int _health = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _health--;
            //Update UI
            other.gameObject.SetActive(false);
        }
    }
}
