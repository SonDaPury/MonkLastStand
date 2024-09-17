using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttackBoss : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground") || other.CompareTag("Door"))
        {
            if (other.CompareTag("Player"))
            {
                PlayerManager.Instance.playerTakeHit.TakeHit("BossAttack");
            }
            Destroy(gameObject);
        }
    }
}
