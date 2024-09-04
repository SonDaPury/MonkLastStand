using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinHealth : MonoBehaviour
{
    public GoblinAI goblinAI;
    public Rigidbody2D rb;

    private void Awake()
    {
        goblinAI = GetComponent<GoblinAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (goblinAI.currentHp <= 0)
        {
            goblinAI.animator.SetTrigger("IsDeath");
            rb.velocity = Vector2.zero;
            Debug.Log(rb.velocity);
            StartCoroutine(DeathCoroutine());
        }
    }

    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
