using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBase : MonoBehaviour
{
    [SerializeField] public int projSpeed;
    [SerializeField] protected int projDmg;
    [SerializeField] protected int lifeTime;
    [SerializeField] protected bool fullAuto;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "IgnoreAll")
        {
            Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
            if (collision.gameObject.tag == "Enemy")
            {
                DealDamage(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "IgnoreAll")
        {
            Debug.Log(gameObject.name + " triggered " + collision.gameObject.name);
            if (collision.gameObject.tag == "Enemy")
            {
                DealDamage(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void DealDamage(GameObject gameObject)
    {
        gameObject.GetComponent<Skull>().TakeDamage(projDmg);
    }
}
