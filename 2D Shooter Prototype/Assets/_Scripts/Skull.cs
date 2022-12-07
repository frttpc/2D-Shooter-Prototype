using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : CharacterBase, IDamageable
{
    private Transform playerPos;
    [SerializeField] private float knockbackValue;

    protected override void Start()
    {
        base.Start();
        if(GameObject.FindGameObjectWithTag("Player"))
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
        if (playerPos)
        {
            Vector2 targetPos = new Vector2(playerPos.position.x, playerPos.position.y + 0.5f);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.TakeDamage(baseDamage);
            PushPlayer(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.TakeDamage(baseDamage);
            PushPlayer(collision.transform);
        }
    }

    private void PushPlayer(Transform player)
    {
        player.GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * knockbackValue, ForceMode2D.Impulse);
    }
}
