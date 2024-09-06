using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class GoblinHealth : MonoBehaviour
{
    public GoblinAI goblinAI;
    public Rigidbody2D rb;
    public GoblinHealthBar goblinHealthBar;
    public BoxCollider2D boxCollider2D;

    private void Awake()
    {
        goblinAI = GetComponent<GoblinAI>();
        rb = GetComponent<Rigidbody2D>();
        goblinHealthBar = GetComponentInChildren<GoblinHealthBar>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        goblinHealthBar.UpdateHealthBar(goblinAI.currentHp, goblinAI.maxHp);
        goblinHealthBar.enabled = true;
    }

    private void Update()
    {
        if (goblinAI.currentHp <= 0)
        {
            goblinAI.animator.SetTrigger("IsDeath");
            rb.velocity = Vector2.zero;
            boxCollider2D.enabled = false;
            StartCoroutine(DeathCoroutine());
            goblinHealthBar.enabled = false;
        }
        else
        {
            goblinHealthBar.enabled = true;
            goblinHealthBar.UpdateHealthBar(goblinAI.currentHp, goblinAI.maxHp);
        }
    }

    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        GoblinSpawn.Instance.OnGoblinDeath(gameObject);
    }

    // Hồi sinh lại Goblin
    public void Respawn()
    {
        goblinAI.currentHp = goblinAI.maxHp;
        goblinHealthBar.UpdateHealthBar(goblinAI.currentHp, goblinAI.maxHp);
        goblinHealthBar.enabled = true;
        boxCollider2D.enabled = true;
        rb.velocity = Vector2.zero;
        gameObject.SetActive(true);
    }
}
