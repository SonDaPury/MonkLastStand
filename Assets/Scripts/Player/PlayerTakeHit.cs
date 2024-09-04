using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerTakeHit : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Material originalMaterial;
        public Material flashMaterial;
        public float flashDuration = 0.1f;

        private Color originalColor;

        private void Awake()
        {
            PlayerManager.Instance.currentHealth = PlayerManager.Instance.maxHealth;
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalMaterial = spriteRenderer.material;
        }

        public void TakeHit()
        {
            PlayerManager.Instance.currentHealth -= 10;
            //PlayerManager.Instance.rb.velocity = Vector2.zero;
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
