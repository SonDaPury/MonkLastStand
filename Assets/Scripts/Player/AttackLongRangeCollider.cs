using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class AttackLongRangeCollider : MonoBehaviour
{
    public Collider2D attackColliderLongRange;
    public SkeletonManager skeletonManager;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerManager.Instance.isEnemyAttack = false;
            GoblinTakeHitLongRange(other);
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
                goblinAI.currentHp -= 30;

                var animator = goblin.GetComponent<Animator>();
                animator.SetTrigger("IsTakeHit");
            }
        }

        foreach (var skeleton in skeletonManager.skeletonSpawn.skeletonsList)
        {
            if (collision.gameObject.Equals(skeleton))
            {
                var skeletonAI = skeleton.GetComponent<SkeletonAi>();
                skeletonAI.currentHp -= 30;

                var animator = skeleton.GetComponent<Animator>();
                animator.SetTrigger("IsTakeHit");
            }
        }
    }
}
