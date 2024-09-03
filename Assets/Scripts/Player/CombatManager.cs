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

    public static CombatManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Combat manager have already been instantiated.");
        }
    }

    // Attack
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && canAttack)
        {
            if (canReceiveInput)
            {
                inputReceived = true;
                canReceiveInput = false;
            }
            else
            {
                return;
            }

            StartCoroutine(AttackCooldownRoutine());
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
