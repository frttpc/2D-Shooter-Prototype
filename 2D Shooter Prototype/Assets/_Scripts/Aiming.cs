using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _aim;
    [SerializeField] private Transform _cursorPosition;
    [SerializeField] private Transform _lookAtAnchor;
    [SerializeField] private Transform _playerPos;
    [SerializeField] private int _maxCursorDistance;
    [SerializeField] [Range(.1f, 1f)] private float _anchorCoeff = 0.5f;

    Vector2 _mousePosition;

    void FixedUpdate()
    {
        MoveAim();
    }

    private void MoveAim()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (_mousePosition - (Vector2)gameObject.transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _aim.transform.eulerAngles = new Vector3(0,0,angle);

        if ((_mousePosition - (Vector2)gameObject.transform.position).sqrMagnitude < _maxCursorDistance * _maxCursorDistance)
        {
            _cursorPosition.position = _mousePosition;
        }

        _lookAtAnchor.position = (_cursorPosition.position - _playerPos.position).normalized * _anchorCoeff;
    }
}
