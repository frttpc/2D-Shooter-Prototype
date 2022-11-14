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
    protected int currentHealth;

    private void Start()
    {
        Debug.Log("Character Base created: " + gameObject.name);
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
            Debug.Log(gameObject + " has died.");
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
