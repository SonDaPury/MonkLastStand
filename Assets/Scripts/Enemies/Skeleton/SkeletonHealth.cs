using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealth : MonoBehaviour
{
    public SkeletonAi skeletonAi;
    public Rigidbody2D rb;
    public SkeletonHealthBar skeletonHealthBar;
    public BoxCollider2D boxCollider2D;

    private void Awake()
    {
        skeletonAi = GetComponent<SkeletonAi>();
        rb = GetComponent<Rigidbody2D>();
        skeletonHealthBar = GetComponentInChildren<SkeletonHealthBar>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        skeletonHealthBar.UpdateHealthBar(skeletonAi.currentHp, skeletonAi.maxHp);
        skeletonHealthBar.enabled = true;
    }

    private void Update()
    {
        if (skeletonAi.currentHp <= 0)
        {
            skeletonAi.animator.SetTrigger("IsDeath");
            rb.velocity = Vector2.zero;
            boxCollider2D.enabled = false;
            skeletonHealthBar.UpdateHealthBar(0, skeletonAi.maxHp);
            StartCoroutine(DeathCoroutine());
            skeletonHealthBar.enabled = false;
        }
        else
        {
            skeletonHealthBar.enabled = true;
            skeletonHealthBar.UpdateHealthBar(skeletonAi.currentHp, skeletonAi.maxHp);
        }
    }

    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        SkeletonSpawn.Instance.OnSkeletonDeath(gameObject);
    }

    public void Respawn()
    {
        skeletonAi.currentHp = skeletonAi.maxHp;
        skeletonHealthBar.UpdateHealthBar(skeletonAi.currentHp, skeletonAi.maxHp);
        skeletonHealthBar.enabled = true;
        boxCollider2D.enabled = true;
        rb.velocity = Vector2.zero;
        gameObject.SetActive(true);
    }
}
