using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerTakeHit : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Material originalMaterial;
        public Material flashMaterial;
        public PlayerHealthBar healthBar;
        public float flashDuration = 0.1f;
        public Text HPText;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalMaterial = spriteRenderer.material;
        }

        private void Start()
        {
            PlayerManager.Instance.currentHealth = PlayerStats.Instance.health;
            healthBar.SetMaxHealth(PlayerStats.Instance.health);
            HPText.text =
                PlayerManager.Instance.currentHealth + " / " + PlayerStats.Instance.health;
        }

        private void Update()
        {
            HPText.text =
                PlayerManager.Instance.currentHealth + " / " + PlayerStats.Instance.health;
        }

        public void TakeHit(string typeEnemy)
        {
            int damage = 0;

            if (typeEnemy.Equals("Goblin") || typeEnemy.Equals("Spike"))
            {
                damage = 20;
            }
            else if (typeEnemy.Equals("Skeleton"))
            {
                damage = 35;
            }
            else if (typeEnemy.Equals("BossAttack"))
            {
                damage = 50;
            }
            else if (typeEnemy.Equals("BossLaser"))
            {
                damage = 60;
            }

            int finalDamage = damage - PlayerStats.Instance.def;

            if (finalDamage <= 0)
            {
                finalDamage = 1;
            }

            PlayerManager.Instance.currentHealth -= finalDamage;

            healthBar.SetHealth(PlayerManager.Instance.currentHealth);
            // HPText.text =
            //     PlayerManager.Instance.currentHealth + " / " + PlayerStats.Instance.health;

            PlayerManager.Instance.playerChangeState._animator.SetBool("IsRunning", false);
            PlayerManager.Instance.playerChangeState._animator.SetBool("IsJumping", false);
            PlayerManager.Instance.playerChangeState._animator.SetBool("IsDowning", false);
            PlayerManager.Instance.playerChangeState._animator.SetTrigger("IsTakeHit");

            StartCoroutine(FlashEffect());
        }

        private IEnumerator FlashEffect()
        {
            spriteRenderer.material = flashMaterial;
            PlayerManager.Instance.attackAction.Disable();

            yield return new WaitForSeconds(flashDuration);
            PlayerManager.Instance.attackAction.Enable();
            spriteRenderer.material = originalMaterial;
        }
    }
}
