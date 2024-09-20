using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandle : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheckLeftLeg;
    public Transform groundCheckRightLeg;
    public LayerMask groundLayer;
    public LayerMask spikeLayer;
    public int maxJumpCount = 2;
    public Chest currentChest;
    public Door currentDoor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckIsAllowJump();
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

    private void CheckIsAllowJump()
    {
        PlayerManager.Instance.isAllowJump =
            Physics2D.OverlapCircle(groundCheckLeftLeg.transform.position, 0.2f, groundLayer)
            || Physics2D.OverlapCircle(groundCheckRightLeg.transform.position, 0.2f, groundLayer)
            || Physics2D.OverlapCircle(groundCheckLeftLeg.transform.position, 0.2f, spikeLayer)
            || Physics2D.OverlapCircle(groundCheckLeftLeg.transform.position, 0.2f, spikeLayer);
    }

    public void OnOpenChest(CallbackContext context)
    {
        if (context.performed)
        {
            if (currentChest != null && currentChest.isPlayerInRange)
            {
                currentChest.OpenChest();
            }
            else if (currentDoor != null && currentDoor.isPlayerInDoorRange)
            {
                currentDoor.OpenDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            currentChest = other.GetComponent<Chest>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            currentChest = null;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            currentDoor = other.gameObject.GetComponent<Door>();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            currentDoor = null;
        }
    }
}
