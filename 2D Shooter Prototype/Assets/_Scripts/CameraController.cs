using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCameraAim;

    void LateUpdate()
    {
        _virtualCameraAim.Priority = Input.GetMouseButton(1) == true ? 5 : 1;
    }
}
