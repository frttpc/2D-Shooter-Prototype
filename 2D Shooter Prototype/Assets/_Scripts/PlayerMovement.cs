using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterBase
{
    [Header("Player")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    private static Vector2 _movement;

    void Update()
    {
        GetInput();
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
}
