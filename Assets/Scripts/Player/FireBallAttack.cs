using System;
using System.Collections;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class FireBallAttack : MonoBehaviour
    {
        public Transform fireballPosition;
        public GameObject fireballPrefab;
        public GameObject cooldownQ;
        public TextMeshProUGUI textCooldownQ;
        public float cooldownTime = 2f;
        public float currentCooldownTime = 0f;
        private bool isCooldown = false;

        private void Update()
        {
            if (isCooldown)
            {
                cooldownQ.SetActive(true);
                currentCooldownTime -= Time.deltaTime;
                textCooldownQ.text = currentCooldownTime.ToString("F0");
            }
            else if (!isCooldown)
            {
                cooldownQ.SetActive(false);
            }
        }

        public void OnFireBallAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (!isCooldown && CombatManager.Instance.playerStamina.currentStamina >= 25f)
                {
                    ShootFireBall();
                    currentCooldownTime = cooldownTime;
                    StartCoroutine(CooldownRoutine());
                }
            }
            if (context.canceled)
            {
                CombatManager.Instance.playerStamina.isStaminaRegen = true;
            }
        }

        private IEnumerator CooldownRoutine()
        {
            isCooldown = true;
            yield return new WaitForSeconds(cooldownTime);
            isCooldown = false;
        }

        private void ShootFireBall()
        {
            Instantiate(fireballPrefab, fireballPosition.position, fireballPosition.rotation);
            AudioManager.Instance.PlaySFX(0);
            CombatManager.Instance.playerStamina.DrainStamina(25f);
        }
    }
}
