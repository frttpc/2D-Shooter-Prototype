using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Campfire : MonoBehaviour
{
    float regenRate = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Light2D>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && regenRate - Time.deltaTime <= 0)
        {
            Player.instance.IncreaseMana(5);
            regenRate = 0.5f;
        }
        regenRate -= Time.deltaTime;
    }
}
