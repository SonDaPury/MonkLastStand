using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Collider2D attackCollider;
    public GameObject goblinPrefab;
    public GoblinManager goblinManager;
    public EnemyManager enemyManager;
    public SkeletonManager skeletonManager;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        goblinManager = goblinPrefab.GetComponent<GoblinManager>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
        skeletonManager = FindAnyObjectByType<SkeletonManager>();
    }

    private void Start()
    {
        attackCollider.enabled = false;
    }

    public void EnableCollider()
    {
        attackCollider.enabled = true;
    }

    public void DisableCollider()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerManager.Instance.isEnemyAttack = false;
            GoblinTakeHitShortRange(other);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            PlayerManager.Instance.isEnemyAttack = true;
        }
    }

    public void GoblinTakeHitShortRange(Collider2D collision)
    {
        foreach (var goblin in EnemyManager.Instance.goblinSpawn.goblinsList)
        {
            if (collision.gameObject.Equals(goblin))
            {
                var goblinAI = goblin.GetComponent<GoblinAI>();
                goblinAI.currentHp -= PlayerStats.Instance.attackDamage;

                var animator = goblin.GetComponent<Animator>();
                animator.SetTrigger("IsTakeHit");
            }
        }

        foreach (var skeleton in skeletonManager.skeletonSpawn.skeletonsList)
        {
            if (collision.gameObject.Equals(skeleton))
            {
                var skeletonAI = skeleton.GetComponent<SkeletonAi>();
                skeletonAI.currentHp -= PlayerStats.Instance.attackDamage;
                var animator = skeleton.GetComponent<Animator>();
                animator.SetTrigger("IsTakeHit");
            }
        }
    }
}
