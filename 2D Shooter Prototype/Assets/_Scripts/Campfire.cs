using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Campfire : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Light2D>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.IncreaseMana(0.2f);
            ManaMeter.Instance.Increase(0.2f);
        }
    }
}
