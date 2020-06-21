using UnityEngine;

public class TurretSpot : MonoBehaviour
{
    [SerializeField] ParticleSystem _placeParticules, _dismantleParticles, _placementParticules;
    [SerializeField] bool _isOccupied;

    private void OnEnable() => HUDController.PlacingTurret += PlacementParticules;
    private void OnDisable() => HUDController.PlacingTurret -= PlacementParticules;

    public void PlaceTower(GameObject turret)
    {
        if (!_isOccupied)
        {
            Turret turretComp = turret.GetComponent<Turret>();
            if ((GameManager.Instance.GetWarFunds() - turretComp.GetWarFundCost()) >= 0)
            {
                GameManager.Instance.ChangeWarFunds(-turretComp.GetWarFundCost());
                _isOccupied = true;
                _placementParticules.gameObject.SetActive(!_isOccupied);
                Debug.Log("Placed");
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
}
