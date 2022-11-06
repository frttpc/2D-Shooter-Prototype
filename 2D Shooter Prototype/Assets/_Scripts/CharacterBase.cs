using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [Header("Charachter Properties")]
    public int maxHealth;
    [SerializeField] protected int currentHealth;
    public int baseDamage;
    public int moveSpeed;
    [SerializeField] protected int attackDistance;

    private void Start()
    {
        Debug.Log(gameObject.name + " is created!");
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
