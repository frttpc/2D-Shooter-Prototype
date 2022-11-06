using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : CharacterBase
{
    public Transform playerPos;
    private Vector2 targetPos;
    [SerializeField] private GameObject _skull;

    private void FixedUpdate()
    {
        MoveTowards();
    }

    protected override void Die()
    {
        Destroy(_skull);
    }

    public void MoveTowards()
    {
        targetPos = new Vector2(playerPos.position.x, playerPos.position.y + 0.5f);
        _skull.transform.position = Vector2.MoveTowards(_skull.transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
    }
}
