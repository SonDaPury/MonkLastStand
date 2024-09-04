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

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        goblinManager = goblinPrefab.GetComponent<GoblinManager>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
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
                goblinAI.currentHp -= 10;

                var animator = goblin.GetComponent<Animator>();
                animator.SetTrigger("IsTakeHit");
            }
        }
    }
}
