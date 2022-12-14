using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Campfire : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("TriggerEnter");
            GetComponent<Light2D>().enabled = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("TriggerStay");
            Player.Instance.IncreaseMana(0.2f);
            ManaMeter.Instance.Increase(0.2f);
        }
    }

}
