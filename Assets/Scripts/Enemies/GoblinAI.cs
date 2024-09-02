using System.Collections;
using UnityEngine;

public class GoblinAI : EnemyAI
{
    protected bool isAttacking = false;
    protected Coroutine attackCoroutine;

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
                AttackPlayer();
            }
        }
        else
        {
            StopAttack();
            patrolEnemy.EnemiesMovement();
            PlayerManager.Instance.playerChangeState.OnTakeHitAnimationEnd();
        }
    }

    protected IEnumerator ContinuousAttack()
    {
        isAttacking = true;

        while (isAttacking)
        {
            PlayerManager.Instance.playerTakeHit.TakeHit();

            yield return new WaitForSeconds(1f);
        }
    }

    public void StopAttack()
    {
        if (isAttacking)
        {
            isAttacking = false;
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }
}
