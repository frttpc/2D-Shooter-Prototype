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
    private static Vector2 _movement;

    private bool _mouse2;

    [SerializeField] private CameraController CamCo;

    private Collider2D[] colliders;

    public static Player instance;

    void Awake()
    {
        if (instance != null)
            instance = this;
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
                if (collider2d.gameObject.tag == "Enemy")
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

        activeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.InverseLerp(100, 1, enemyDistSqr) * 5;
    }

    public void DecreaseMana(int amount)
    {
        if(instance._currentMana > 0)
        {
            if (instance._currentMana - amount < 0)
                instance._currentMana = 0;
            else
                instance._currentMana -= amount;
        }
    }

    public void IncreaseMana(int amount)
    {
        if (instance._currentMana < instance._maxMana)
        {
            if (instance._currentMana + amount > instance._maxMana)
                instance._currentMana = instance._maxMana;
            else
                instance._currentMana += amount;
        }
    }

    public void DecreaseLook(float amount)
    {
        if (instance._currentLook > 0)
        {
            if (instance._currentLook - amount < 0)
                instance._currentLook = Mathf.Lerp(instance._currentLook, 0, 1);
            else
                instance._currentLook = Mathf.Lerp(instance._currentLook, instance._currentLook - amount, 1);
        }
    }
}
