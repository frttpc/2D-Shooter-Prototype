using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : CharacterBase
{
    [Header("Camera")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _virtualCameraPlayer;
    [SerializeField] private CinemachineVirtualCamera _virtualCameraAim;

    [Header("Cursor and Anchor")]
    [SerializeField] private Transform cursorPosition;
    [SerializeField] private Transform lookAtAnchor;
    [SerializeField] private int maxCursorDistance;
    [SerializeField] [Range(.1f, 1f)] private float anchorCoeff = 0.5f; 

    private Vector2 _mousePosition;
    private bool _mouse1Clicked;
    private bool _mouse2Clicked;
    private float _mouse3;

    [Header("Player")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    private Vector2 _movement;

    void Update()
    {
        GetInput();

        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        ZoomInOut();

        AnimatePlayer();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);

        //Vector2 lookDirection = (_mousePosition - rb.position).normalized;
        //float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        MoveCursorandAnchor();

        _virtualCameraAim.Priority = _mouse2Clicked == true ? 5 : 1;
    }

    private void GetInput()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _mouse1Clicked = Input.GetButtonDown("Fire1");
        _mouse2Clicked = Input.GetButton("Fire2");
        _mouse3 = Input.mouseScrollDelta.y;
    }

    private void AnimatePlayer()
    {
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    private void MoveCursorandAnchor()
    {
        if ((_mousePosition - rb.position).sqrMagnitude > maxCursorDistance * maxCursorDistance)
            cursorPosition.position = rb.position + (_mousePosition - rb.position).normalized * maxCursorDistance;
        else
            cursorPosition.position = _mousePosition;

        lookAtAnchor.position = rb.position + ((Vector2)cursorPosition.position - rb.position) * anchorCoeff;
    }

    private void ZoomInOut()
    {
        float orthoSize = _virtualCameraPlayer.m_Lens.OrthographicSize;

        if (_mouse3 > 0 && orthoSize < 10)
        {
            orthoSize -= 1;
        }
        else if (_mouse3 < 0 && orthoSize > 5)
        {
            orthoSize += 1;
        }

        _virtualCameraPlayer.m_Lens.OrthographicSize = Mathf.Clamp(orthoSize, 5, 10);
    }
}
