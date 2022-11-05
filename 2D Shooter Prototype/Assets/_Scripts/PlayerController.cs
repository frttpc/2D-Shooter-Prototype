using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : CharacterBase
{
    [Space]
    [Header("Camera")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _virtualCameraPlayer;
    [SerializeField] private CinemachineVirtualCamera _virtualCameraAim;

    [Header("Aim")]
    [SerializeField] private Transform _cursorPosition;
    [SerializeField] private GameObject _aim;
    [SerializeField] private Transform _lookAtAnchor;
    [SerializeField] private int _maxCursorDistance;
    [SerializeField] [Range(.1f, 1f)] private float _anchorCoeff = 0.5f; 

    private Vector2 _mousePosition;
    private bool _mouse1;
    private bool _mouse2;
    private float _mouse3;

    [Header("Player")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _moveSpeed;
    private Vector2 _movement;

    [Space]
    public AudioSource audioSource;

    [Space]
    public GameObject spellcastPrefab;
    public float spellcastForce;

    void Update()
    {
        GetInput();
        
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = _mousePosition - _rb.position;
        
        MoveAim(lookDirection);
        Shoot(lookDirection);

        ZoomInOut();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();

        _virtualCameraAim.Priority = _mouse2 == true ? 5 : 1;
    }

    private void GetInput()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _mouse1 = Input.GetButtonDown("Fire1");
        _mouse2 = Input.GetButton("Fire2");
        _mouse3 = Input.mouseScrollDelta.y;
    }

    private void MovePlayer()
    {
        if(_movement.sqrMagnitude > 0)
        {
            _rb.MovePosition(_rb.position + _movement.normalized * _moveSpeed * Time.fixedDeltaTime);
            //audioSource.enabled = true;
        }
        else
            audioSource.enabled = false;
    }

    private void MoveAim(Vector2 lookDir)
    {
        if (lookDir.sqrMagnitude > _maxCursorDistance * _maxCursorDistance)
            _cursorPosition.position = _rb.position + lookDir.normalized * _maxCursorDistance;
        else
            _cursorPosition.position = _mousePosition;

        _lookAtAnchor.position = _rb.position + ((Vector2)_cursorPosition.position - _rb.position) * _anchorCoeff;

        _firePoint.position = lookDir.normalized + _rb.position;
    }

    private void Shoot(Vector2 lookDir)
    {
        if (_mouse1)
        {
            GameObject bullet = Instantiate(spellcastPrefab, _firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce((_movement + lookDir).normalized * spellcastForce, ForceMode2D.Impulse);
        }
    }

    private void ZoomInOut()
    {
        float orthoSize = _virtualCameraPlayer.m_Lens.OrthographicSize;

        if(_mouse3 != 0)
        {
            if (_mouse3 < 0 && orthoSize < 10)
            {
                orthoSize += 1;
            }
            else if (_mouse3 > 0 && orthoSize > 5)
            {
                orthoSize -= 1;
            }

            _virtualCameraPlayer.m_Lens.OrthographicSize = Mathf.Clamp(orthoSize, 5, 10);
        }
    }

    private void AnimatePlayer()
    {
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }
}
