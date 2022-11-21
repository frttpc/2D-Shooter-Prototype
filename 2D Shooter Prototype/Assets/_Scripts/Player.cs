using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : CharacterBase
{
    [SerializeField] private int _maxMana;
    [field:SerializeField] public int _currentMana { get; private set; }
    [SerializeField] private float _maxLook;
    [field:SerializeField] public float _currentLook { get; private set; }

    [Header("Player")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private CameraController CamCo;
    private static Vector2 _movement;

    private bool _mouse2;
    private Collider2D[] colliders;

    public static Player Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("Already a Player instance!");
    }

    void Start()
    {
        _currentMana = _maxMana;
        _currentLook = _maxLook;
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
        _mouse2 = Input.GetMouseButton(1);
    }

    private void MovePlayer()
    {
        if (_movement.sqrMagnitude > 0)
            _rb.MovePosition(_rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void CheckForEnemy()
    {
        GameObject closestEnemy = FindClosestEnemyOverlapCircle();

        if (closestEnemy == null)
        {
            ShakeCamera(Mathf.Infinity);
        }
        else
        {
            ShakeCamera((closestEnemy.transform.position - transform.position).sqrMagnitude);
        }
    }

    private GameObject FindClosestEnemyOverlapCircle()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        float closestDistance = Mathf.Infinity * Mathf.Infinity;
        GameObject closestEnemy = null;

        if (colliders.Length != 0)
        {
            foreach (Collider2D collider2d in colliders)
            {
                if (collider2d.gameObject.CompareTag("Enemy"))
                {
                    float tempDistSqr = (collider2d.transform.position - transform.position).sqrMagnitude;

                    if (tempDistSqr < closestDistance)
                    {
                        closestDistance = tempDistSqr;
                        closestEnemy = collider2d.gameObject;
                    }
                }
            }
        }
        return closestEnemy;
    }

    public Vector2 GetMovement()
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
        CinemachineVirtualCamera activeCam;

        if (_mouse2)
        {
            activeCam = CamCo.GetCam(1).VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        }
        else
            activeCam = CamCo.GetCam(0).VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();

        activeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.InverseLerp(100, 1, enemyDistSqr) * 3;
    }

    public void DecreaseMana(int amount)
    {
        if(_currentMana > 0)
        {
            if (_currentMana - amount < 0)
                _currentMana = 0;
            else
                _currentMana -= amount;
        }
    }

    public void IncreaseMana(int amount)
    {
        if (_currentMana < _maxMana)
        {
            if (_currentMana + amount > _maxMana)
                _currentMana = _maxMana;
            else
                _currentMana += amount;
        }
    }

    public void DecreaseLook(float amount)
    {
        if (_currentLook > 0)
        {
            if (_currentLook - amount < 0)
                _currentLook = Mathf.Lerp(_currentLook, 0, 1);
            else
                _currentLook = Mathf.Lerp(_currentLook, _currentLook - amount, 1);
        }
    }

    public int GetHealth()
    {
        return maxHealth;
    }

    public float GetLook()
    {
        return _maxLook;
    }
}
