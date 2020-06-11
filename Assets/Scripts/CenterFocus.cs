using System;
using System.Collections;

using UnityEngine;

public class CenterFocus : MonoBehaviour
{
    Vector3 moveBy;

    [SerializeField] float _speed = 1;
    private void OnEnable() => CameraController.moveCenterFocus += DoMoveCenterFocus;
    private void OnDisable() => CameraController.moveCenterFocus -= DoMoveCenterFocus;

    void DoMoveCenterFocus(Vector3 moveBys)
    {
        moveBy = moveBys;
    }

    private void FixedUpdate()
    {
        if (moveBy != Vector3.zero)
        {
            Moving();
        }
    }

    void Moving()
    {
        Debug.LogError("MOVE");
        if (transform.position.x + moveBy.x < -20 && transform.position.x + moveBy.x > -30)
            transform.position += new Vector3(moveBy.x * Time.fixedDeltaTime * _speed, 0, 0);

        if (transform.position.y + moveBy.y < -11 && transform.position.y + moveBy.y > -20)
            transform.position += new Vector3(0, moveBy.y, 0);

        if (transform.position.z + moveBy.z < 8.5 && transform.position.z + moveBy.z > -1.5)
            transform.position += new Vector3(0, 0, moveBy.z * Time.fixedDeltaTime * _speed);

    }
    
}
