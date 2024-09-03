using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 6f; // Speed when move of player
    public Vector2 moveInput; // A variable to receive input form input system unity
    public float jumpForceOnce = 7f; // lực nhảy lần 1
    public float jumpForceTwice = 7f; // lực nhảy lần 2
    public PlayerInputHandle playerHandleInput; // Get PlayerHandleInput Instance
    public PlayerChangeState playerChangeState; // Get PlayerChangeState Instance
    public PlayerTakeHit playerTakeHit; // Get PlayerTakeHit Instance
    public int jumpCount = 0;
    public bool isAllowJump;
    public int maxHealth = 100;
    public int currentHealth;
    public Rigidbody2D rb;
    private bool _canMove = true;

    public static PlayerManager Instance { get; private set; } // Singleton pattern

    private void Awake()
    {
        playerHandleInput = GetComponent<PlayerInputHandle>();
        playerChangeState = GetComponent<PlayerChangeState>();
        playerTakeHit = GetComponent<PlayerTakeHit>();
        rb = GetComponent<Rigidbody2D>();

        if (Instance == null)
        {
            // Nếu chưa có, gán instance này và không phá hủy nó khi chuyển đổi scene
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Nếu đã có instance, phá hủy đối tượng này để giữ nguyên instance duy nhất.
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!_canMove)
            return;
        playerHandleInput.MovePlayerHorizontal(moveInput, moveSpeed);
        playerChangeState.ChangeStateMovementOfPlayer();
    }

    public void SetCanMove(bool value)
    {
        _canMove = value;
    }

    public void OnMoveInput(CallbackContext input)
    {
        moveInput = input.ReadValue<Vector2>();
    }

    // Get jump input from input system unity
    public void OnJumpInput(CallbackContext input)
    {
        if (input.started)
        {
            playerHandleInput.SingleJumpAction(jumpForceOnce, input, jumpForceTwice);
        }
    }

    public void Attack(CallbackContext context)
    {
        CombatManager.Instance.Attack(context);
    }
}
