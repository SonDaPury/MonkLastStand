using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public GameObject goblinPrefab;
    public GameObject flyingEyePrefab;
    public Vector2[] goblinSpawnPoint;
    public Vector2[] skeletonSpawnPoint;
    public Vector2[] flyingEyeSpawnPoint;
    public int goblinCount = 0;
    public int skeletonCount = 0;
    public int flyingEyeCount = 0;

    public float spawnDelay = 0f;

    public EnemyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
