using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public bool canReceiveInput;
    public bool inputReceived;
    private bool canAttack = true;
    public float attackCooldown = 1f;
    public bool isAttacking = false;
    public PlayerStamina playerStamina;

    public static CombatManager Instance { get; private set; }

    private void Awake()
    {
        playerStamina = FindAnyObjectByType<PlayerStamina>();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Attack
    public void Attack(InputAction.CallbackContext context)
    {
        if (
            context.performed
            && canAttack
            && playerStamina.currentStamina >= playerStamina.staminaDrainRate
        )
        {
            if (canReceiveInput)
            {
                isAttacking = true;
                inputReceived = true;
                canReceiveInput = false;
                playerStamina.DrainStamina(10f);
            }
            else
            {
                return;
            }

            StartCoroutine(AttackCooldownRoutine());
        }

        if (context.canceled)
        {
            isAttacking = false;
            playerStamina.isStaminaRegen = true;
        }
    }

    public void InputManager()
    {
        if (!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else
        {
            canReceiveInput = false;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
