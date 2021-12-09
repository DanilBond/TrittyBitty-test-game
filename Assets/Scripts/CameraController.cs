using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _cameraSpeed;

    bool _canMove;
    public bool CanMove { get => _canMove; set => _canMove = value; }

    void Update()
    {
        if(_canMove)
            RotateCameraAround();
    }

    void RotateCameraAround()
    {
        if (Input.GetMouseButton(0))
        {
            float hor = Input.GetAxis("Mouse X") * Time.deltaTime * _cameraSpeed;
            gameObject.transform.RotateAround(Vector3.zero, Vector3.up, hor);
        }
    }
}
