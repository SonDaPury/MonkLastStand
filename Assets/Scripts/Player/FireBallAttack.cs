using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class FireBallAttack : MonoBehaviour
    {
        public Transform fireballPosition;
        public GameObject fireballPrefab;
        public float cooldownTime = 2f;
        private bool isCooldown = false;

        public void OnFireBallAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (!isCooldown)
                {
                    ShootFireBall();
                    StartCoroutine(CooldownRoutine());
                }
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
        }
    }
}
