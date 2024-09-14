using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public bool isChase = false;
    public GameObject player;
    public Transform door;
    public LayerMask groundLayer;
    public float moveSpeedBoss = 5f;
    public Rigidbody2D rb;

    [SerializeField]
    protected Transform groundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        BossMovement();

        if (IsWallCollision())
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void BossMovement()
    {
        rb.velocity = new Vector2(moveSpeedBoss, rb.velocity.y);
    }

    public void BossChasePlayer() { }

    public void BossIsChasePlayer()
    {
        var directionToPlayer = (player.transform.position - transform.position).normalized;
    }

    public bool IsWallCollision()
    {
        return Physics2D.Raycast(groundCheck.position, transform.right, 1f, groundLayer) == true;
    }
}
