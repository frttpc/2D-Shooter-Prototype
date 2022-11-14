using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        CheckForDestroy();
    }

    private void CheckForDestroy()
    {
        if (_currentHealth < 0)
            Destroy(gameObject);
    }
}
