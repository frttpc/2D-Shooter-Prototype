using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : CharacterBase
{
    public Rigidbody2D rb;
    public Transform playerPos;

    private void Update()
    {
        MoveTowards();
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);

    }

    public void MoveTowards()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, moveSpeed * Time.fixedDeltaTime);
    }
}
