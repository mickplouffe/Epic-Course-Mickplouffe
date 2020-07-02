using System;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoSingleton<HUDController>
{
    GameObject _placeHolder;
    [SerializeField] GameObject TurretInPool1; //CHANGE IT!
    [SerializeField] GameObject TurretInPool2; //CHANGE IT!
    [SerializeField] Text _livesCount, _wavesCount, _warFundCount;


    public static Action<bool> PlacingTurret;


    private void Start()
    {
        UpdateHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (_placeHolder != null)
        {
            PlaceHolderFolowing();
        }
    }

    private void PlaceHolderFolowing()
    {
        if (_placeHolder != null)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.transform.tag == "TurretSpot")
                {

                    _placeHolder.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + .5f, hitInfo.transform.position.z);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_placeHolder.name == "Gatling_Gun_PlaceHolder(Clone)")
                        {
                            hitInfo.transform.gameObject.GetComponent<TurretSpot>().PlaceTower(TurretInPool1);
                        }
                        else
                        {
                            hitInfo.transform.gameObject.GetComponent<TurretSpot>().PlaceTower(TurretInPool2);
                        }
                    }
                }
                else
                {
                    _placeHolder.transform.position = hitInfo.point;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_placeHolder != null)
            {
                PlacingTurret?.Invoke(false);

                Destroy(_placeHolder.gameObject);
            }
        }
    }

    public void BuyTurret(GameObject turret)
    {
        PlacingTurret?.Invoke(true);

        if (_placeHolder != null)
        {
            if (turret.name != _placeHolder.name)
            {
                Destroy(_placeHolder);
                _placeHolder = Instantiate(turret);
            }
        }
        else
        {
            _placeHolder = Instantiate(turret);
        }
    }

    public void UpdateHUD(string nameToUpdate = null)
    {
        switch (nameToUpdate)
        {
            case "lives":
                _livesCount.text = TheHQ.Instance.GetHealth().ToString();
                break;

            case "waves":
                //_wavesCount.text = WaveManager.Instance.GetCurrentWave().ToString();
                break;

            case "funds":
                _warFundCount.text = GameManager.Instance.GetWarFunds().ToString();
                break;

            default:
                _livesCount.text = TheHQ.Instance.GetHealth().ToString();
                //_wavesCount.text = WaveManager.Instance.GetCurrentWave().ToString();
                _warFundCount.text = GameManager.Instance.GetWarFunds().ToString();
                break;
        }
    }
}

