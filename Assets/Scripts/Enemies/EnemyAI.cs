using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    protected Transform player;

    [SerializeField]
    protected float moveSpeed = 5f;

    [SerializeField]
    protected float attackRange = 1.5f;

    [SerializeField]
    protected LayerMask playerLayer;

    [SerializeField]
    protected bool isChasing = false;

    [SerializeField]
    protected Rigidbody2D rb;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    protected void Update()
    {
        if (isChasing)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
                AttackPlayer();
            }
        }
    }

    protected void AttackPlayer() { }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with player");
        if (collision.gameObject.CompareTag("Player"))
        {
            isChasing = true;
        }
    }
}
