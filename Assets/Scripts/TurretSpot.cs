using UnityEngine;

public class TurretSpot : MonoBehaviour
{
    [SerializeField] ParticleSystem _placeParticules, _dismantleParticles, _placementParticules;
    [SerializeField] bool _isOccupied;

    private void OnEnable()
    {
        HUDController.PlacingTurret += PlacementParticules;
        GameManager.resetGameEvent += ResetTurretSpot;

    }

    private void OnDisable()
    {
        HUDController.PlacingTurret -= PlacementParticules;
        GameManager.resetGameEvent -= ResetTurretSpot;

    }

    public void PlaceTower(GameObject turret)
    {
        if (!_isOccupied)
        {
            Turret turretComp = turret.GetComponent<Turret>();
            if ((GameManager.Instance.GetWarFunds() - turretComp.GetWarFundCost()) >= 0)
            {
                GameManager.Instance.AddWarFunds(-turretComp.GetWarFundCost());

                _isOccupied = true;
                _placementParticules.gameObject.SetActive(!_isOccupied);
                GameObject Turret = Instantiate(turret, transform);
                Turret.transform.position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);

                if (_placeParticules != null)
                {
                    ParticleSystem PlaceParticles = Instantiate(_placeParticules);
                    PlaceParticles.transform.position = transform.position;
                }
            }
        }
    }

    public void UpgradeTower()
    {
        if (transform.childCount == 2)
        {
            GameObject turretUpgrade = transform.GetChild(1).GetComponent<Turret>().UpgradeTurret;
            Turret turretComp = turretUpgrade.GetComponent<Turret>();
            if ((GameManager.Instance.GetWarFunds() - turretComp.GetWarFundCost()) >= 0)
            {
                GameManager.Instance.AddWarFunds(-turretComp.GetWarFundCost());
                _isOccupied = true;
                _placementParticules.gameObject.SetActive(!_isOccupied);
                GameObject Turret = Instantiate(turretUpgrade, transform);
                Turret.transform.position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);

                DestroyCurrentTower();

                if (_placeParticules != null)
                {
                    ParticleSystem PlaceParticles = Instantiate(_placeParticules);
                    PlaceParticles.transform.position = transform.position;
                }
            }
        }        
    }

    public void DestroyCurrentTower()
    {
        if (transform.childCount == 3)
        {
            Destroy(transform.GetChild(1).transform.gameObject);
        }
    }

    public void DestroyTower()
    {
        if (transform.childCount > 1)
        {
            GameManager.Instance.AddWarFunds(Mathf.RoundToInt(transform.GetChild(1).GetComponent<Turret>().GetWarFundCost()/2));
            
            Destroy(transform.GetChild(1).transform.gameObject);

            _isOccupied = false;

            _placementParticules.gameObject.SetActive(!_isOccupied);


        }
    }

    public bool GetOccupied()
    {
        return _isOccupied;
    }

    void PlacementParticules(bool isActiving)
    {
        if (isActiving)
        {
            if (_placementParticules != null)
            {
                _placementParticules.Play();
            }
        }
        else
        {
            if (_placementParticules != null)
            {
                _placementParticules.Stop();
            }
        }
    }

    void ResetTurretSpot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != 0)
            {
                Destroy(transform.GetChild(i).transform.gameObject);

            }
        }
        _isOccupied = false;

        _placementParticules.gameObject.SetActive(!_isOccupied);
    }
}
