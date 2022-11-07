using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Camera main;
    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;

    void LateUpdate()
    {
        _virtualCameras[1].Priority = Input.GetMouseButton(1) == true ? 5 : 1;
    }

    public CinemachineVirtualCamera GetCam(int camNo)
    {
        return camNo == 0 ? _virtualCameras[0] : _virtualCameras[1];
    }
}
