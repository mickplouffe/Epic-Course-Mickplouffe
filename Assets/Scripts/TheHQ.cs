using System;
using UnityEngine;

public class TheHQ : MonoSingleton<TheHQ>
{
    [SerializeField] int _health = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _health--;
            if (_health < 0)
                _health = 0;
            HUDController.Instance.UpdateHUD("lives");            
            other.gameObject.SetActive(false);
        }
    }

    public int GetHealth()
    {
        return _health;
    }
}
