using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damageInterval = 1f;
    private bool isPlayerInTrap = false;
    private Coroutine damageCoroutine;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isPlayerInTrap)
            {
                isPlayerInTrap = true;
                damageCoroutine = StartCoroutine(ApplyDamageOverTime(collision.gameObject));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInTrap = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator ApplyDamageOverTime(GameObject player)
    {
        while (isPlayerInTrap)
        {
            PlayerManager.Instance.playerTakeHit.TakeHit();
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
