using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public float flySpeed = 3f;
    public GameObject rockAttackPrefab;
    public Transform rockSpawnPoint;
    public GameObject rockProjectile;
    public Rigidbody2D rbRockProjectile;
    public float direction;

    private void FixedUpdate()
    {
        if (rbRockProjectile != null)
        {
            rbRockProjectile.velocity = new Vector2(
                direction * flySpeed,
                rbRockProjectile.velocity.y
            );
        }
    }

    public void ShootProjectileRock()
    {
        GameObject rockProjectile = Instantiate(
            rockAttackPrefab,
            rockSpawnPoint.position,
            Quaternion.identity
        );

        rbRockProjectile = rockProjectile.GetComponent<Rigidbody2D>();

        if (transform.localScale.x < 0)
        {
            direction = -1f;
            rockProjectile.transform.localScale = new Vector3(
                -1 * Mathf.Abs(rockProjectile.transform.localScale.x),
                transform.localScale.y,
                1
            );
        }
        else
        {
            direction = 1f;
            rockProjectile.transform.localScale = new Vector3(
                1 * Mathf.Abs(rockProjectile.transform.localScale.x),
                transform.localScale.y,
                1
            );
        }
    }
}
