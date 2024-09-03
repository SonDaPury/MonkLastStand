using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    protected Transform player;

    [SerializeField]
    protected float moveSpeed = 5f;

    [SerializeField]
    protected float attackRange = 3f;

    [SerializeField]
    protected LayerMask playerLayer;

    [SerializeField]
    protected bool isChasing = false;

    [SerializeField]
    protected Vector2 direction;

    [SerializeField]
    protected Rigidbody2D rb;

    [SerializeField]
    protected PatrolEnemy patrolEnemy;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform.Find("CenterOfBody");
        patrolEnemy = GetComponent<PatrolEnemy>();
    }

    protected void FixedUpdate()
    {
        CheckIsChase();
        ChasePlayer();
    }

    public virtual void AttackPlayer() { }

    protected virtual void ChasePlayer()
    {
        if (isChasing)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                Vector2 directionPlayer = (player.position - transform.position).normalized;
                direction = new Vector2(directionPlayer.x, 0f);

                transform.localScale = new Vector3(
                    (direction.x > 0 ? 1f : -1f) * Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    1f
                );

                patrolEnemy.isFaceRight = direction.x > 0;

                if (IsPlayerInDetectionArea())
                {
                    rb.velocity = direction * moveSpeed;
                }
            }
            else
            {
                AttackPlayer();
            }
        }
        else
            patrolEnemy.EnemiesMovement();
    }

    protected void CheckIsChase()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        RaycastHit2D detectPlayer = Physics2D.Raycast(
            transform.position,
            directionToPlayer,
            7f,
            playerLayer
        );

        Debug.DrawRay(transform.position, directionToPlayer * 7f, Color.red);

        if (detectPlayer.collider != null && IsPlayerInDetectionArea())
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }

    protected bool IsPlayerInDetectionArea()
    {
        float distanceToPlayer = Mathf.Abs(transform.position.y - player.position.y);
        return distanceToPlayer <= 1f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
