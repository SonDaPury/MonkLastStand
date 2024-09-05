using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealth : MonoBehaviour
{
    public SkeletonAi skeletonAi;
    public Rigidbody2D rb;
    public SkeletonHealthBar skeletonHealthBar;

    private void Awake()
    {
        skeletonAi = GetComponent<SkeletonAi>();
        rb = GetComponent<Rigidbody2D>();
        skeletonHealthBar = GetComponentInChildren<SkeletonHealthBar>();
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
        Destroy(gameObject);
    }
}
