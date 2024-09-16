using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float meleeRange = 2f;
    public float rangedAttackRange = 5f;
    public float moveSpeed = 3f;
    public float attackCooldown = 2f;

    public bool isAttacking = false;
    private float distanceToPlayer;
    public Rigidbody2D rb;
}
