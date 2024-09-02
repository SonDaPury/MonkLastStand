using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeState : MonoBehaviour
{
    public Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeStateMovementOfPlayer()
    {
        if (Mathf.Abs(PlayerManager.Instance.moveInput.x) > 0 && PlayerManager.Instance.isAllowJump)
        {
            _animator.SetBool("IsRunning", true);
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsDowning", false);
        }
        else if (
            !PlayerManager.Instance.isAllowJump
            && PlayerManager.Instance.playerHandleInput.rb.velocity.y > 0
        )
        {
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsJumping", true);
            _animator.SetBool("IsDowning", false);
        }
        else if (
            !PlayerManager.Instance.isAllowJump
            && PlayerManager.Instance.playerHandleInput.rb.velocity.y < 0
        )
        {
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsDowning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsDowning", false);
        }
    }

    public void PlayerTakeHit(bool isTakeHit)
    {
        if (isTakeHit)
        {
            _animator.SetBool("IsTakeHit", true);
            PlayerManager.Instance.SetCanMove(false);
        }
        else
        {
            _animator.SetBool("IsTakeHit", false);
            PlayerManager.Instance.SetCanMove(true);
        }
    }
}
