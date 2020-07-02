using UnityEngine;

public class CenterFocus : MonoBehaviour
{
    Vector3 moveBy;

    [SerializeField] float _speed = 1;
    private void OnEnable() => CameraController.moveCenterFocus += DoMoveCenterFocus;
    private void OnDisable() => CameraController.moveCenterFocus -= DoMoveCenterFocus;

    void DoMoveCenterFocus(Vector3 moveBy)
    {
        if (transform.position.x + moveBy.x < -16 && transform.position.x + moveBy.x > -34)
            transform.position += new Vector3(moveBy.x * GameManager.Instance.GetFixedTimestep() * _speed, 0, 0);

        if (transform.position.y + moveBy.y < -11 && transform.position.y + moveBy.y > -20)
            transform.position += new Vector3(0, moveBy.y / 2, 0);

        if (transform.position.z + moveBy.z < 25 && transform.position.z + moveBy.z > -3.2)
            transform.position += new Vector3(0, 0, moveBy.z * GameManager.Instance.GetFixedTimestep() * _speed);
    }    
}
