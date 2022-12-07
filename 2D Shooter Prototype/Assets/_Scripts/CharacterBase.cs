using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour, IDamageable
{
    [Header("Charachter Properties")]
    [SerializeField] protected int baseDamage;
    [SerializeField] protected int moveSpeed;
    [SerializeField] protected int attackDistance;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
