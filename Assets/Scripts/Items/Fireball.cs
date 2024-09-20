using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Enemies;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireballSpeed = 10f;
    public GameObject impactEffect;
    private Rigidbody2D rb;
    public SkeletonManager skeletonManager;
    public BossBehaviour bossBehaviour;
    public GameObject boss;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GameObject.Find("Boss");
        skeletonManager = FindAnyObjectByType<SkeletonManager>();
    }

    private void Start()
    {
        bossBehaviour = boss.GetComponent<BossBehaviour>();
        rb.velocity = new Vector2(PlayerManager.Instance.transform.localScale.x * fireballSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.CompareTag("Enemy")
            || collision.CompareTag("Ground")
            || collision.CompareTag("Door")
            || collision.CompareTag("Boss")
        )
        {
            var fireballImpact = Instantiate(impactEffect, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX(2);
            Destroy(gameObject);
            Destroy(fireballImpact, 0.25f);
        }

        foreach (var goblin in EnemyManager.Instance.goblinSpawn.goblinsList)
        {
            if (collision.gameObject.Equals(goblin))
            {
                var goblinAI = goblin.GetComponent<GoblinAI>();
                goblinAI.currentHp -= PlayerStats.Instance.fireballDamage;

                var animator = goblin.GetComponent<Animator>();
                animator.SetTrigger("IsTakeHit");
            }
        }

        foreach (var skeleton in skeletonManager.skeletonSpawn.skeletonsList)
        {
            if (collision.gameObject.Equals(skeleton))
            {
                var skeletonAI = skeleton.GetComponent<SkeletonAi>();

                skeletonAI.currentHp -= PlayerStats.Instance.fireballDamage;
                var animator = skeleton.GetComponent<Animator>();
                animator.SetTrigger("IsTakeHit");
            }
        }

        if (collision.gameObject.Equals(boss))
        {
            bossBehaviour.currentHp -= PlayerStats.Instance.fireballDamage;
        }
    }
}
