using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : CharacterBase
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _virtualCameraPlayer;
    [SerializeField] private CinemachineVirtualCamera _virtualCameraAim;

    public Transform cursorPosition;

    [SerializeField] private float moveSpeed;
    private Vector2 _movement;
    private Vector2 _mousePosition;
    private bool _mouse1Clicked;
    private bool _first = true;
    public Transform lookAtAnchor;
    public int maxDistanceForAnchor;


    public Rigidbody2D rb;
    public Animator animator;

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _mouse1Clicked = Input.GetButton("Fire1");

        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (_mouse1Clicked)
        {
            Transform temp = _mousePosition.po;
        }

        cursorPosition.position = _mousePosition;

        lookAtAnchor.position = (rb.position + _mousePosition) * 0.5f;

        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);

        if (_mouse1Clicked)
        {
            _virtualCameraAim.Priority = 5;
            //Debug.Log("LA FU: " + lookAtAnchor);
            //Vector2 lookDirecton = _mousePosition - rb.position;
            //float angle = Mathf.Atan2(lookDirecton.y, lookDirecton.x) * Mathf.Rad2Deg - 90f;
        }
        else
        {
            _virtualCameraAim.Priority = 1;
            _first = true;
            //rb.rotation = 0;
        }
    }
}
