using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float speed;

    private float direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction = Input.GetAxis("Horizontal");

        if (direction < 0) _sprite.flipX = false;
        if (direction > 0) _sprite.flipX = true;
    }

    private void FixedUpdate()
    {
        var position = _rb.position;
        position.x += direction * speed * Time.deltaTime;

        _rb.MovePosition(position);

        _animator.SetFloat("speed", Mathf.Abs(direction));
    }
}
