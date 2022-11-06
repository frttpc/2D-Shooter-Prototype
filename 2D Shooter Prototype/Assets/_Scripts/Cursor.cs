using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _defaultCursor;
    [SerializeField] private Sprite _EnemyCursor;
    [SerializeField] private Sprite _shootableCursor;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            _spriteRenderer.sprite = _EnemyCursor;
        }
        else if(collision.transform.tag == "Shootable")
        {
            _spriteRenderer.sprite = _shootableCursor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.sprite = _defaultCursor;
    }
}
