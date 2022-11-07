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
                DamageEnemy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "Shootable")
            {
                DamageObject(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "IgnoreAll" && collision.gameObject.tag != "Player")
        {
            Debug.Log(gameObject.name + " triggered " + collision.gameObject.name);
            if (collision.gameObject.tag == "Enemy")
            {
                DamageEnemy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "Shootable")
            {
                DamageObject(collision.gameObject);
            }
            DestroyObject();
        }
    }

    private void DamageEnemy(GameObject gameObject)
    {
        gameObject.GetComponent<Skull>().TakeDamage(projDmg);
    }

    private void DamageObject(GameObject gameObject)
    {
        gameObject.GetComponent<ShootableObjects>().TakeDamage(projDmg);
    }

    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }
}
