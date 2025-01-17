using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class AttackLongRangeCollider : MonoBehaviour
{
    public Collider2D attackColliderLongRange;
    public SkeletonManager skeletonManager;
    public BossBehaviour bossBehaviour;
    public GameObject boss;
    public BossHealthBar bossHealthBar;

    private void Awake()
    {
        skeletonManager = FindAnyObjectByType<SkeletonManager>();
        attackColliderLongRange = GetComponent<Collider2D>();
    }

    private void Start()
    {
        attackColliderLongRange.enabled = false;
    }

    public void EnableCollider()
    {
        attackColliderLongRange.enabled = true;
    }

    public void DisableCollider()
    {
        attackColliderLongRange.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerManager.Instance.isEnemyAttack = false;
            GoblinTakeHitLongRange(other);
        }
        else if (other.CompareTag("Boss"))
        {
            if (other.gameObject.Equals(boss))
            {
                if (!bossBehaviour.isBossDefend)
                {
                    bossBehaviour.currentHp -= PlayerStats.Instance.attackDamage;
                    bossHealthBar.SetHealth(bossBehaviour.currentHp);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            PlayerManager.Instance.isEnemyAttack = true;
        }
    }

    public void GoblinTakeHitLongRange(Collider2D collision)
    {
        foreach (var goblin in EnemyManager.Instance.goblinSpawn.goblinsList)
        {
            if (collision.gameObject.Equals(goblin))
            {
                var goblinAI = goblin.GetComponent<GoblinAI>();
                var patrolGoblin = goblin.GetComponent<PatrolEnemy>();

                if (!patrolGoblin.IsEgde() && !patrolGoblin.IsWallCollision())
                {
                    goblinAI.currentHp -= 30;

                    var animator = goblin.GetComponent<Animator>();
                    animator.SetTrigger("IsTakeHit");
                }

                //goblinAI.currentHp -= 30;

                //var animator = goblin.GetComponent<Animator>();
                //animator.SetTrigger("IsTakeHit");
            }
        }

        foreach (var skeleton in skeletonManager.skeletonSpawn.skeletonsList)
        {
            if (collision.gameObject.Equals(skeleton))
            {
                var skeletonAI = skeleton.GetComponent<SkeletonAi>();
                var patrolSkeleton = skeleton.GetComponent<PatrolEnemy>();

                if (!patrolSkeleton.IsEgde() && !patrolSkeleton.IsWallCollision())
                {
                    skeletonAI.currentHp -= 30;

                    var animator = skeleton.GetComponent<Animator>();
                    animator.SetTrigger("IsTakeHit");
                }

                //    skeletonAI.currentHp -= 30;

                //    var animator = skeleton.GetComponent<Animator>();
                //    animator.SetTrigger("IsTakeHit");
            }
        }
    }
}
