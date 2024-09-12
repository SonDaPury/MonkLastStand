using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ShieldSkill : MonoBehaviour
{
    public float cooldownTime = 3f;
    public bool isCooldown = false;
    public float currentCooldownTime = 0f;
    public int shieldValue = 5;
    public float shieldDuration = 5f;
    public GameObject sheildPrefab;
    public GameObject sheildPosition;
    public GameObject shieldInstance;
    public GameObject cooldownR;
    public TextMeshProUGUI textCooldownR;
    public bool isShieldEnd = true;

    private void Update()
    {
        if (shieldInstance != null)
            shieldInstance.transform.position = sheildPosition.transform.position;

        if (isCooldown && isShieldEnd)
        {
            cooldownR.SetActive(true);
            currentCooldownTime -= Time.deltaTime;
            textCooldownR.text = currentCooldownTime.ToString("F0");
        }
        else
        {
            cooldownR.SetActive(false);
        }
    }

    public void OnUseShield(CallbackContext context)
    {
        if (context.performed)
        {
            if (!isCooldown)
            {
                isCooldown = true;
                ShieldInstance();
                isShieldEnd = false;
                currentCooldownTime = cooldownTime;
                PlayerStats.Instance.def += shieldValue;
                StartCoroutine(ShieldDurationRoutine());
            }
        }
    }

    private IEnumerator ShieldDurationRoutine()
    {
        yield return new WaitForSeconds(shieldDuration);
        PlayerStats.Instance.def -= shieldValue;
        DestroyShield();
        isShieldEnd = true;
        StartCoroutine(CooldownRoutine());
    }

    private void DestroyShield()
    {
        Destroy(shieldInstance);
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    private void ShieldInstance()
    {
        shieldInstance = Instantiate(
            sheildPrefab,
            sheildPosition.transform.position,
            sheildPosition.transform.rotation
        );
    }
}
