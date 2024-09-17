using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBoss : MonoBehaviour
{
    public GameObject attackMeleePrefab;
    public BoxCollider2D boxCollider2D;
    public MeleeAttack meleeAttack;

    private void Start()
    {
        boxCollider2D = attackMeleePrefab.GetComponent<BoxCollider2D>();
        meleeAttack = attackMeleePrefab.GetComponent<MeleeAttack>();
    }

    public void MeleeAttackCollision()
    {
        boxCollider2D.enabled = true;
    }

    public void MeleeAttackEnd()
    {
        boxCollider2D.enabled = false;
    }
}
