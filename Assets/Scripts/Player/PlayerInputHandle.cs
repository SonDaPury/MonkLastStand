using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandle : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public int maxJumpCount = 2;
    public int pressSpaceCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckIsAllowJump();
        PlayerManager.Instance.playerChangeState.ChangeStateMovementOfPlayer();
    }

    // Di chuyển nhân vật theo phương ngang
    public void MovePlayerHorizontal(Vector2 moveInput, float moveSpeed)
    {
        // Move player right-left by rigidbody2d velocity
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        // Quay người khi người chơi ấn a hoặc d
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector2(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector2(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }
    }

    public void SingleJumpAction(float jumpForce, CallbackContext input, float jumpForceTwice)
    {
        if (PlayerManager.Instance.isAllowJump)
        {
            PlayerManager.Instance.jumpCount = 0;
        }

        if (PlayerManager.Instance.jumpCount < maxJumpCount)
        {
            if (PlayerManager.Instance.jumpCount == 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else if (PlayerManager.Instance.jumpCount == 1)
            {
                rb.velocity = Vector2.up * jumpForceTwice;
            }
            PlayerManager.Instance.jumpCount++;
        }
    }

    public void CheckIsAllowJump()
    {
        PlayerManager.Instance.isAllowJump = Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
        );
    }
}
