using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserAttack : MonoBehaviour
{
    public GameObject laserLinePrefab;
    public Transform laserSpawnPoint;
    public float laserLifeTime = 1f;
    public LayerMask collisionLayer;
    private GameObject activeLaser; // Tia laser đang hoạt động
    private Vector2 targetPosition; // Vị trí của player tại thời điểm bắn
    private Vector2 direction; // Hướng của tia laser
    private bool isLaserActive = false; // Trạng thái của tia laser

    public void ShootLaser(Vector2 playerPosition)
    {
        targetPosition = playerPosition;
        StartCoroutine(ActivateLaser());
    }

    private IEnumerator ActivateLaser()
    {
        direction = (targetPosition - (Vector2)laserSpawnPoint.position).normalized;
        // RaycastHit2D hit = Physics2D.Raycast(
        //     laserSpawnPoint.position,
        //     direction,
        //     Mathf.Infinity,
        //     collisionLayer
        // );

        // float laserLength;
        // if (hit.collider != null)
        // {
        //     laserLength = Vector2.Distance(laserSpawnPoint.position, hit.point);
        // }
        // else
        // {
        //     laserLength = Vector2.Distance(laserSpawnPoint.position, targetPosition);
        // }

        activeLaser = Instantiate(laserLinePrefab, laserSpawnPoint.position, Quaternion.identity);
        // activeLaser.transform.localScale = new Vector3(
        //     laserLength,
        //     activeLaser.transform.localScale.y,
        //     1
        // );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        activeLaser.transform.rotation = Quaternion.Euler(0, 0, -angle);

        isLaserActive = true;
        PlayerManager.Instance.playerTakeHit.TakeHit("BossLaser");
        yield return new WaitForSeconds(laserLifeTime);

        Destroy(activeLaser);
        isLaserActive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isLaserActive)
        {
            Gizmos.DrawLine(laserSpawnPoint.position, targetPosition);
        }
    }
}
