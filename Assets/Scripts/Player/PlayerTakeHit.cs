using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class PlayerTakeHit : MonoBehaviour
{
    public float knockBackForce = 5f;
    public float knockBackTime = 0.25f;
    public GameObject centerOfBody;

    void Awake()
    {
        PlayerManager.Instance.currentHealth = PlayerManager.Instance.maxHealth;
        centerOfBody = GameObject.Find("CenterOfBody");
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Enemy")) { }
    // }

    public void TakeHit()
    {
        PlayerManager.Instance.currentHealth -= 10;
        PlayerManager.Instance.rb.velocity = Vector2.zero;
        PlayerManager.Instance.playerChangeState.PlayerTakeHit();
    }
}
