using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject _spellcastPrefab;
    private ProjectileBase projectileBase;
    [SerializeField] private Transform _firePoint;

    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        projectileBase = _spellcastPrefab.GetComponent<ProjectileBase>();
    }

    private void Update()
    {
        Vector2 _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = _mousePosition - (Vector2)gameObject.transform.position;

        if (Input.GetMouseButtonDown(0))
            if(Player.Instance._currentMana > 0)
                Shoot(_spellcastPrefab, lookDirection, _firePoint.position, Player.Instance.GetMovement());
    }

    public void Shoot(GameObject projectile, Vector2 lookDir, Vector2 firePoint, Vector2 movement)
    {
        GameObject bullet = Instantiate(projectile, firePoint, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce((movement + lookDir).normalized * projectileBase.projSpeed, ForceMode2D.Impulse);

        Player.Instance.DecreaseMana(projectileBase.projCost);
    }
}
