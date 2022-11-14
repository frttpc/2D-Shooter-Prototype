using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;

    [SerializeField] private Light2D light2D;

    public void SlowlyDecreaseLightFallout()
    {
        if (light2D.intensity > 5)
        {
            light2D.intensity -= Time.deltaTime * 0.1f;
        }
    }
}
