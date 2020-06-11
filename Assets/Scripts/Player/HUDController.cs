using UnityEngine;

public class HUDController : MonoBehaviour
{
    GameObject _placeHolder;

    // Update is called once per frame
    void Update()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (_placeHolder != null && Physics.Raycast(rayOrigin, out hitInfo))
        {
            if (hitInfo.transform.tag == "TurretSpot")
            {
                _placeHolder.transform.position = hitInfo.transform.position;
            }
            else
            {
                _placeHolder.transform.position = hitInfo.point;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_placeHolder != null)
            {
                Destroy(_placeHolder.gameObject);
            }
        }
    }

    public void BuyTurret(GameObject turret)
    {
        //call turret to the  turretManager
        //if (GameManager.warFund > turret.cost)
        //{

        //}

        if (_placeHolder == null)
        {
            _placeHolder = Instantiate(turret);
        }
    }
}

