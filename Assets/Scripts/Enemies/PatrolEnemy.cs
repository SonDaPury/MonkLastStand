using System;
using UnityEngine;

namespace Enemies
{
  public class PatrolEnemy : MonoBehaviour
  {
    [SerializeField] protected Rigidbody2D rg;
    [SerializeField] protected float moveSpeedEnemy = 5f;
    [SerializeField] protected Vector2 moveDirection = Vector2.right;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    public bool isFaceRight = true;

    private void Awake()
    {
      rg = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
      // EnemiesMovement();

      var hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);

      if (hit.collider != false)
      {
        if (isFaceRight)
        {
          rg.velocity = new Vector2(moveSpeedEnemy, rg.velocity.y);
        }
        else
        {
          rg.velocity = new Vector2(-moveSpeedEnemy, rg.velocity.y);

        }
      }
      else
      {
        isFaceRight = !isFaceRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
      }
    }

    protected void EnemiesMovement()
    {
      rg.velocity = new Vector2(moveDirection.x * moveSpeedEnemy, rg.velocity.y);
      // Debug.Log(moveDirection);

      // if (IsEgde())
      // {
      //   Flip();
      // }
    }

    // Check xem enemy có ở edge của platform không
    protected bool IsEgde()
    {
      return Physics2D.Raycast(transform.position, moveDirection, 1f, LayerMask.GetMask("Ground")).collider == null;
    }

    protected void Flip()
    {
      moveDirection *= -1;
    }
  }
}
