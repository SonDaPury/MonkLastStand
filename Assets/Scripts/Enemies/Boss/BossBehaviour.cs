using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public GameObject player;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float moveSpeedBoss = 5f;
    public float chaseRange = 10f; // Khoảng cách tối đa để boss bắt đầu đuổi theo
    public float meleeRange = 2f; // Phạm vi tấn công cận chiến
    public float rangedAttackRange = 5f; // Phạm vi tấn công tầm xa
    public float attackCooldown = 5f; // Thời gian chờ giữa các lần tấn công
    public bool isAttacking = false;
    public Vector2 directionToPlayer;

    private Rigidbody2D rb;
    public Animator animator;
    private float distanceToPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        directionToPlayer = (player.transform.position - transform.position).normalized;
        transform.localScale = new Vector3(
            (directionToPlayer.x > 0 ? 1f : -1f) * Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            1f
        );

        if (distanceToPlayer <= meleeRange)
        {
            StartCoroutine(MeleeAttack());
        }
        else if (distanceToPlayer <= rangedAttackRange)
        {
            StartCoroutine(RangedAttack());
        }
        else if (distanceToPlayer <= chaseRange && !isAttacking)
        {
            BossChasePlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsAttackMelee", false);
            animator.SetBool("IsAttackRange", false);
        }

        if (IsWallCollision())
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Đuổi theo player
    private void BossChasePlayer()
    {
        rb.velocity = new Vector2(directionToPlayer.x * moveSpeedBoss, rb.velocity.y);
    }

    // Tấn công cận chiến
    private IEnumerator MeleeAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero; // Dừng lại khi tấn công
            Debug.Log("Melee Attack!");

            // Thực hiện hành động tấn công (thêm animation hoặc logic tấn công)
            animator.SetBool("IsAttackMelee", true);
            animator.SetBool("IsAttackRange", false);

            yield return new WaitForSeconds(attackCooldown); // Chờ giữa các lần tấn công
            isAttacking = false;
        }
    }

    // Tấn công tầm xa
    private IEnumerator RangedAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero; // Dừng lại khi tấn công
            Debug.Log("Ranged Attack!");

            // Thực hiện hành động tấn công (thêm animation hoặc logic tấn công tầm xa)
            animator.SetBool("IsAttackMelee", false);
            animator.SetBool("IsAttackRange", true);

            yield return new WaitForSeconds(attackCooldown); // Chờ giữa các lần tấn công
            isAttacking = false;
        }
    }

    private bool IsWallCollision()
    {
        return Physics2D.Raycast(groundCheck.position, transform.right, 1f, groundLayer);
    }
}
