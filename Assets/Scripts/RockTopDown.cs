using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTopDown : MonoBehaviour
{
    public Vector2 firstPosition;
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 direction;

    [SerializeField]
    protected LayerMask groundLayer;

    [SerializeField]
    protected GameObject groundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        firstPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (transform.position.Equals(firstPosition))
        {
            direction = Vector2.down;
        }

        rb.velocity = new Vector2(rb.velocity.x, direction.y * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            direction = Vector2.up;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PointCheck"))
        {
            direction = Vector2.down;
        }
    }
}
