using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private Light2D _light;
    [SerializeField] private float _startTime = 0.2f;
    [SerializeField] private float _flickerSpeed = 0.1f;

    private void Start() {
        InvokeRepeating(nameof(Flicker), _startTime, _flickerSpeed);
    }

    private void Flicker()
    {
        _light.intensity = Random.Range(.5f, 1.5f);
    }
}
