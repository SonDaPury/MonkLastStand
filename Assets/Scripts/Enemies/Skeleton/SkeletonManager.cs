using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public List<Vector2> skeletonSpawnPoint;
    public SkeletonManager skeletonManager;
    public SkeletonSpawn skeletonSpawn;

    private void Awake()
    {
        skeletonManager = GetComponent<SkeletonManager>();
        skeletonSpawn = GetComponent<SkeletonSpawn>();
    }

    //private void Start()
    //{
    //    skeletonSpawnPoint.Add(new Vector2(-4.22f, -15.58f));
    //    skeletonSpawnPoint.Add(new Vector2(6.86f, -16.75f));
    //}

    private void Reset()
    {
        skeletonSpawnPoint.Add(new Vector2(-4.22f, -15.58f));
        skeletonSpawnPoint.Add(new Vector2(6.86f, -16.75f));
    }
}
