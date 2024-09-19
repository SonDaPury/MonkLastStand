using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    public GameObject player;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float moveSpeedBoss = 5f;
    public float chaseRange = 10f; // Khoảng cách tối đa để boss bắt đầu đuổi theo
    public float meleeRange = 2f; // Phạm vi tấn công cận chiến
    public float rangedAttackRange = 5f; // Phạm vi tấn công tầm xa
    public float attackCooldown = 5f; // Thời gian chờ giữa các lần tấn công
    public bool isAttacking = false;
    public int countAttack = 0;
    public Vector2 directionToPlayer;
    public GameObject energyAccumulationBoss;
    public GameObject energyAccumulationBossPosition;
    public BossLaserAttack bossLaserAttack;
    public float maxHpBoss = 500f;
    public float currentHp = 0;
    public bool isBossDefend = false;
    public BossHealthBar bossHealthBar;
    public Text hpBossText;
    public GameObject healthBarBoss;

    private Rigidbody2D rb;
    public Animator animator;
    private float distanceToPlayer;
    public static BossBehaviour Instance { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bossLaserAttack = GetComponent<BossLaserAttack>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHp = maxHpBoss;
        bossHealthBar.SetMaxHealth(maxHpBoss);
        hpBossText.text = currentHp + " / " + maxHpBoss;
        healthBarBoss.SetActive(false);
    }

    private void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        directionToPlayer = (player.transform.position - transform.position).normalized;

        transform.localScale = new Vector3(
            (directionToPlayer.x > 0 ? 1f : -1f) * Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            1f
        );

        if (!isAttacking)
        {
            if ((distanceToPlayer <= meleeRange) && countAttack < 2)
            {
                StartCoroutine(MeleeAttack());
            }
            else if ((distanceToPlayer <= rangedAttackRange) && countAttack < 2)
            {
                StartCoroutine(RangedAttack());
            }
            else if ((countAttack >= 2) && (distanceToPlayer <= chaseRange))
            {
                countAttack = 0;
                StartCoroutine(LaserAttack());
            }
            else if ((distanceToPlayer <= chaseRange) && !isAttacking)
            {
                BossChasePlayer();
            }
            else
            {
                rb.velocity = Vector2.zero;
                animator.SetBool("IsAttackMelee", false);
                animator.SetBool("IsAttackRange", false);
            }
        }

        if (IsWallCollision())
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        hpBossText.text = currentHp + " / " + maxHpBoss;
    }

    private IEnumerator LaserAttack()
    {
        rb.velocity = Vector2.zero;
        isAttacking = true;
        animator.SetBool("IsLaserFace", true);
        animator.SetBool("IsAttackMelee", false);
        animator.SetBool("IsAttackRange", false);
        energyAccumulationBoss.transform.position = energyAccumulationBossPosition
            .transform
            .position;

        yield return new WaitForSeconds(0.7f);

        energyAccumulationBoss.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        animator.SetBool("IsLaserFace", false);
        energyAccumulationBoss.SetActive(false);

        bossLaserAttack.ShootLaser(player.transform.position);
        yield return new WaitForSeconds(1.5f);

        yield return new WaitForSeconds(attackCooldown); // Chờ cooldown trước khi tấn công lại
        isAttacking = false;
        // animator.SetBool("IsLaserFace", false);
    }

    // Đuổi theo player
    private void BossChasePlayer()
    {
        rb.velocity = new Vector2(directionToPlayer.x * moveSpeedBoss, rb.velocity.y);
    }

    // Tấn công cận chiến
    private IEnumerator MeleeAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero; // Dừng lại khi tấn công
            countAttack++;

            // Thực hiện hành động tấn công (thêm animation hoặc logic tấn công)
            animator.SetBool("IsAttackMelee", true);
            animator.SetBool("IsAttackRange", false);
            animator.SetBool("IsLaserFace", false);

            yield return new WaitForSeconds(1.5f);
            animator.SetBool("IsAttackMelee", false);

            yield return new WaitForSeconds(attackCooldown - 1.5f); // Chờ giữa các lần tấn công
            isAttacking = false;
        }
    }

    // Tấn công tầm xa
    private IEnumerator RangedAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero; // Dừng lại khi tấn công
            countAttack++;

            // Thực hiện hành động tấn công (thêm animation hoặc logic tấn công tầm xa)
            animator.SetBool("IsAttackMelee", false);
            animator.SetBool("IsAttackRange", true);
            animator.SetBool("IsLaserFace", false);

            yield return new WaitForSeconds(1.3f); // Giả định thời gian thực hiện đòn đánh tầm xa là 1 giây

            animator.SetBool("IsAttackRange", false);
            // Chờ thêm thời gian cooldown trước khi có thể tấn công lại
            yield return new WaitForSeconds(attackCooldown - 1.3f);
            isAttacking = false;
        }
    }

    private bool IsWallCollision()
    {
        return Physics2D.Raycast(groundCheck.position, transform.right, 1f, groundLayer);
    }
}
