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
            PlayerManager.Instance.currentHealth = PlayerManager.Instance.maxHealth;
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalMaterial = spriteRenderer.material;
        }

        private void Start()
        {
            healthBar.SetMaxHealth(PlayerManager.Instance.maxHealth);
            HPText.text =
                PlayerManager.Instance.currentHealth + " / " + PlayerManager.Instance.maxHealth;
        }

        public void TakeHit()
        {
            PlayerManager.Instance.currentHealth -= 10;
            healthBar.SetHealth(PlayerManager.Instance.currentHealth);
            HPText.text =
                PlayerManager.Instance.currentHealth + " / " + PlayerManager.Instance.maxHealth;
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
