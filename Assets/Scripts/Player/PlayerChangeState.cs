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

  private void Update()
  {
    if (PlayerManager.Instance != null)
    {
      // Chuyển trạng thái giữa đứng im và chạy và ngược lại
      SwitchIdleRun();
    }

    if (
        PlayerManager.Instance.isAllowJump
        && Mathf.Abs(PlayerManager.Instance.playerHandleInput.rb.velocity.y) < 0.1f
    )
    {
      if (Mathf.Abs(PlayerManager.Instance.playerHandleInput.rb.velocity.x) > 0.1f)
      {
        _animator.SetBool("IsRunning", true);
        _animator.SetBool("IsJumping", false);
      }
      else
      {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsJumping", false);
      }
    }
  }

  private void SwitchIdleRun()
  {
    // Lấy tốc độ di chuyển từ PlayerManager
    float moveSpeed = PlayerManager.Instance.moveInput.x;

    // Nếu người chơi đang di chuyển sang trái hoặc phải
    if (Mathf.Abs(moveSpeed) > 0.1f && PlayerManager.Instance.isAllowJump)
    {
      _animator.SetBool("IsRunning", true);
    }
    else
    {
      _animator.SetBool("IsRunning", false);
    }
  }

  public void PlayerJumping()
  {
    _animator.SetBool("IsJumping", true);
    _animator.SetBool("IsDowning", false);
    _animator.SetBool("IsRunning", false);
  }

  public void PlayerIsFalling()
  {
    _animator.SetBool("IsJumping", false);
    _animator.SetBool("IsDowning", true);
    _animator.SetBool("IsRunning", false);
  }

  public void ChangeStateMovementOfPlayer()
  {
    if (
        !PlayerManager.Instance.isAllowJump
        && PlayerManager.Instance.playerHandleInput.rb.velocity.y < 0
    )
    {
      PlayerIsFalling();
    }
    else if (
        !PlayerManager.Instance.isAllowJump
        && PlayerManager.Instance.playerHandleInput.rb.velocity.y > 0
    )
    {
      PlayerJumping();
    }
    else if (
        PlayerManager.Instance.isAllowJump
        && PlayerManager.Instance.playerHandleInput.rb.velocity == Vector2.zero
    )
    {
      _animator.SetBool("IsDowning", false);
      _animator.SetBool("IsRunning", false);
    }
    else if (
        PlayerManager.Instance.isAllowJump
        && PlayerManager.Instance.playerHandleInput.rb.velocity != Vector2.zero
    )
    {
      _animator.SetBool("IsDowning", false);
      _animator.SetBool("IsRunning", true);
    }
  }
}
