using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBase : MonoBehaviour
{
    [SerializeField] public int projSpeed { get; private set; }
    [SerializeField] public int projCost { get; private set; }
    [SerializeField] protected int projDmg;
    [SerializeField] protected int lifeTime;
    [SerializeField] protected bool fullAuto;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(name + " collided with " + collision.gameObject.name);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damage(damageable);
        }
        DestroyObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(name + " triggered " + collision.name);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        Debug.Log("isDamageable?: " + damageable);
        if (damageable != null)
        {
            Damage(damageable);
        }
        DestroyObject();
    }

    private void Damage(IDamageable damageableObject)
    {
        damageableObject.TakeDamage(projDmg);
    }

    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }
}
