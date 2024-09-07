using System;
using System.Collections;
using Enemies;
using UnityEngine;

public class GoblinAI : EnemyAI
{
    protected bool isAttacking = false;
    protected Coroutine attackCoroutine;
    public Animator animator;
    public int maxHp = 100;
    public int currentHp;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Reset()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        if (currentHp <= 0)
        {
            PlayerStats.Instance.AddExperience(30);
        }
    }

    public override void AttackPlayer()
    {
        if (!isAttacking)
        {
            attackCoroutine = StartCoroutine(ContinuousAttack());
        }
    }

    protected override void ChasePlayer()
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
                else
                {
                    StopAttack();
                }
            }
            else
            {
                if (PlayerManager.Instance.isEnemyAttack && currentHp > 0)
                {
                    AttackPlayer();
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    StopAttack();
                    rb.velocity = Vector2.zero;
                }
            }
        }
        else
        {
            StopAttack();
            patrolEnemy.EnemiesMovement();
            if (currentHp <= 0)
                return;
        }
    }

    private IEnumerator ContinuousAttack()
    {
        isAttacking = true;

        while (isAttacking)
        {
            animator.SetTrigger("IsAttack");

            yield return new WaitForSeconds(1f);
            PlayerManager.Instance.playerTakeHit.TakeHit("Goblin");
        }
    }

    private void StopAttack()
    {
        if (!isAttacking)
            return;
        if (attackCoroutine == null)
            return;

        isAttacking = false;
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerManager.Instance.playerTakeHit.TakeHit("Goblin");
    }
}
