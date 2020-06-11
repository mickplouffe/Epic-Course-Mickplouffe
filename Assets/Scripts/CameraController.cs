using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    public static Action<Vector3> moveCenterFocus;
    [SerializeField] List<GameObject> turretPlaceHolders;

    [SerializeField] int borderMargin = 5;
    GameObject _placeHolder;
    Vector2 mousePosition;


    public void OnPan(InputAction.CallbackContext ctx)
    {
        if (ctx.started || ctx.canceled)
        {
            Vector2 axis = ctx.ReadValue<Vector2>();
            moveCenterFocus(new Vector3(axis.x, 0, axis.y));
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
        Debug.Log(ctx.ReadValue<Vector2>().ToString());
        if (ctx.ReadValue<Vector2>().y > 0 && ctx.started)
        {
            moveCenterFocus(Vector3.down / 2);
        }

        if (ctx.ReadValue<Vector2>().y < 0 && ctx.started)
        {
            moveCenterFocus(Vector3.up / 2);
        }

    }
}
