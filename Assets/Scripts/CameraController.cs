using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// NEED REDO
/// </summary>
public class CameraController : MonoBehaviour
{
    public static Action<Vector3> moveCenterFocus;
    [SerializeField] List<GameObject> turretPlaceHolders;

    [SerializeField] int borderMargin = 5;
    GameObject _placeHolder;
    public Vector2 mousePosition;
    Vector2 panningAxis;

    public static Action DeselectAll;

    private void Start()
    {
        //controlScheme = ;
        StartCoroutine(HoldingPan());
    }

    private void Update()
    {
        ClickOnScreen();
    }


    public void OnPan(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            panningAxis = ctx.ReadValue<Vector2>();
            //StartCoroutine(HoldingPan());
        }
        else if (ctx.canceled)
        {
            panningAxis = ctx.ReadValue<Vector2>();
            //StopCoroutine(HoldingPan());
        }

    }

    public void OnPoint(InputAction.CallbackContext ctx)
    {
        mousePosition = ctx.ReadValue<Vector2>();
        if (mousePosition.x < borderMargin && mousePosition.x >= 0)
        {
            moveCenterFocus(Vector3.left);
        }
        if (mousePosition.x > Screen.width - borderMargin && mousePosition.x <= Screen.width)
        {
            moveCenterFocus(Vector3.right);
        }
        if (mousePosition.y > Screen.height - borderMargin && mousePosition.y <= Screen.height)
        {
            moveCenterFocus(Vector3.forward);
        }
        if (mousePosition.y < borderMargin && mousePosition.y >= 0)
        {
            moveCenterFocus(Vector3.back);
        }
        //if(!(position.x < borderMargin && position.x >= 0 && position.x > Screen.width - 30 && position.x <= Screen.width && position.y > Screen.height - 30 && position.y <= Screen.height && position.y < 30 && position.y >= 0))
        //    moveCenterFocus(Vector3.zero);

    }

    public void OnZoom(InputAction.CallbackContext ctx)
    {
        if (ctx.started || ctx.canceled)
        {
            if (ctx.ReadValue<Vector2>().y > 0)
            {
                moveCenterFocus(Vector3.down);
            }

            if (ctx.ReadValue<Vector2>().y < 0)
            {
                moveCenterFocus(Vector3.up);
            }
        }
    }


    private void ClickOnScreen()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                DeselectAll?.Invoke();

                if (hitInfo.transform.tag == "TurretSpot")
                {
                    if (hitInfo.transform.childCount == 2)
                    {
                        if (hitInfo.transform.GetChild(1))
                        {
                            hitInfo.transform.GetChild(1).GetComponent<Turret>().OnSelect();
                            //hitInfo.transform.GetComponent<TurretSpot>().UpgradeTower();
                        }
                        //Select turret
                    }
                }

                if (hitInfo.transform.name == "UpgradeYes")
                    hitInfo.transform.GetComponentInParent<TurretSpot>().UpgradeTower();

                else if (hitInfo.transform.name == "DismantleYes")
                    hitInfo.transform.GetComponentInParent<TurretSpot>().DestroyTower();
            }
        }
    }

    IEnumerator HoldingPan()
    {
        while (true)
        {
            moveCenterFocus(new Vector3(panningAxis.x, 0, panningAxis.y));
            yield return new WaitForSecondsRealtime(.05f);
        }

    }
}
