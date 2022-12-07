using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBase : MonoBehaviour
{
    [field:SerializeField] public int projSpeed { get; private set; }
    [field:SerializeField] public int projCost { get; private set; }
    [SerializeField] protected int projDmg;
    [SerializeField] protected int lifeTime;
    [SerializeField] protected bool fullAuto;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null && !collision.gameObject.CompareTag("Player"))
        {
            Damage(damageable);
        }
        DestroyObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null && !collision.CompareTag("Player"))
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
