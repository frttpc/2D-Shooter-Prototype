using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : CharacterBase, IDamageable
{
    private Transform playerPos;

    private void Start()
    {
        currentHealth = maxHealth;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        MoveTowards();
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    public void MoveTowards()
    {
        Vector2 targetPos = new Vector2(playerPos.position.x, playerPos.position.y + 0.5f);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
    }
}
