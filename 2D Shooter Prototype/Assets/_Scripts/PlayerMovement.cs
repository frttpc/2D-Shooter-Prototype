using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : CharacterBase
{
    [Header("Player")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    private static Vector2 _movement;

    [SerializeField] private CameraController CamCo;
    private CinemachineBasicMultiChannelPerlin CBMCP;
    private List<GameObject> _enemiesInsideRadius;

    private void Awake()
    {
        CBMCP = CamCo.GetCam(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _enemiesInsideRadius = new List<GameObject>();
    }

    void Update()
    {
        GetInput();

        CheckForEnemy();

        AnimatePlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        if (_movement.sqrMagnitude > 0)
            _rb.MovePosition(_rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void CheckForEnemy()
    {
        if (_enemiesInsideRadius.Count == 0)
            ShakeCamera(Mathf.Infinity);
        else
        {
            ShakeCamera(FindClosestEnemyDistanceSqr());
        }
    }


    private float FindClosestEnemyDistanceSqr()
    {
        GameObject closestEnemy;
        float closestDistance = Mathf.Infinity * Mathf.Infinity;

        foreach (GameObject enemy in _enemiesInsideRadius)
        {
            float tempDist = (enemy.transform.position - gameObject.transform.position).sqrMagnitude;
            if (tempDist < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = tempDist;
            }
        }
        return closestDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            _enemiesInsideRadius.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _enemiesInsideRadius.Remove(collision.gameObject);
        }
    }

    public static Vector2 GetMovement()
    {
        return _movement;
    }

    private void AnimatePlayer()
    {
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);

        //Animate damage effect
    }

    private void ShakeCamera(float enemyDistSqr)
    {
        CBMCP.m_AmplitudeGain = Mathf.InverseLerp(100, 1, enemyDistSqr) * 5;
    }
}
