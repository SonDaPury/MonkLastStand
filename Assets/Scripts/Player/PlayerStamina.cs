using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float currentStamina;
    public float staminaDrainRate = 10f; // Số lượng stamina mất khi tấn công
    public float staminaRegenRate = 5f; // Số lượng stamina hồi phục mỗi giây
    public float staminaRegenDelay = 2f; // Thời gian chờ trước khi stamina bắt đầu hồi phục
    public Slider staminaSlider;
    public bool isStaminaRegen = true;
    public Coroutine regenCoroutine;
    public Animator animator;

    private void Start()
    {
        currentStamina = PlayerStats.Instance.stamina;
        staminaSlider.maxValue = PlayerStats.Instance.stamina;
        staminaSlider.value = currentStamina;
    }

    void Update()
    {
        if (
            animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")
            && currentStamina < PlayerStats.Instance.stamina
            && isStaminaRegen
        )
        {
            StaminaRegen();
        }
    }

    public void DrainStamina()
    {
        isStaminaRegen = false;
        if (currentStamina >= staminaDrainRate)
        {
            currentStamina -= staminaDrainRate;
            staminaSlider.value = currentStamina;

            if (regenCoroutine != null)
            {
                StopCoroutine(regenCoroutine);
                regenCoroutine = null;
            }
        }
    }

    public void StaminaRegen()
    {
        if (isStaminaRegen && regenCoroutine == null)
        {
            regenCoroutine = StartCoroutine(StaminaRegenRoutine());
        }
    }

    private IEnumerator StaminaRegenRoutine()
    {
        yield return new WaitForSeconds(staminaRegenDelay);

        while (currentStamina < PlayerStats.Instance.stamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            staminaSlider.value = currentStamina;

            currentStamina = Mathf.Min(currentStamina, PlayerStats.Instance.stamina);

            yield return null;
        }

        regenCoroutine = null;
    }
}
