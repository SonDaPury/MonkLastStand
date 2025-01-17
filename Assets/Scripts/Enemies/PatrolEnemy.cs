using System;
using UnityEngine;

namespace Enemies
{
    public class PatrolEnemy : MonoBehaviour
    {
        public Rigidbody2D rg;
        public bool isFaceRight = true;
        public float moveSpeedEnemy = 3f;
        public LayerMask groundLayer;

        [SerializeField]
        protected Vector2 moveDirection = Vector2.right;

        [SerializeField]
        protected Transform groundCheck;

        private void Awake()
        {
            rg = GetComponent<Rigidbody2D>();
        }

        public void EnemiesMovement()
        {
            if (!IsEgde() && !IsWallCollision())
            {
                Flip();
                rg.velocity = new Vector2(moveSpeedEnemy * moveDirection.x, rg.velocity.y);
            }
            else
            {
                isFaceRight = !isFaceRight;
                transform.localScale = new Vector3(
                    -transform.localScale.x,
                    transform.localScale.y,
                    1f
                );
            }
        }

        // Check xem enemy có ở edge của platform không
        public bool IsEgde()
        {
            return Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer).collider
                == false;
        }

        // Check xem enemy có ở tường của platform không
        public bool IsWallCollision()
        {
            return Physics2D.Raycast(groundCheck.position, transform.right, 1f, groundLayer)
                == true;
        }

        // Đổi hướng di chuyển của enemy
        public void Flip()
        {
            if (isFaceRight)
            {
                moveDirection = Vector2.right;
            }
            else
            {
                moveDirection = Vector2.left;
            }
        }
    }
}
