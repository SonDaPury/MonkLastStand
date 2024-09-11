using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireballSpeed = 10f;
    public GameObject impactEffect;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(PlayerManager.Instance.transform.localScale.x * fireballSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.CompareTag("Enemy")
            || collision.CompareTag("Ground")
            || collision.CompareTag("Door")
        )
        {
            var fireballImpact = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(fireballImpact, 0.25f);
        }
    }
}
