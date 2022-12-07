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
        if (Input.GetMouseButton(1))
        {
            _virtualCameras[1].Priority = 5;
            LookMeter.Instance.Decrease(0.2f);
        }
        else
        {
            _virtualCameras[1].Priority = 1;
            LookMeter.Instance.Increase();

        }
    }

    public CinemachineVirtualCamera GetCam(int camNo)
    {
        return camNo == 0 ? _virtualCameras[0] : _virtualCameras[1];
    }

    public CinemachineBrain GetBrain()
    {
        return main.GetComponent<CinemachineBrain>();
    }
}
