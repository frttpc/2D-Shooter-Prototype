using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Campfire : MonoBehaviour
{
    float regenRate = 1f;

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
            Player.Instance.IncreaseMana(5);
            regenRate = 1f;
        }
        regenRate -= Time.deltaTime;
    }
}
