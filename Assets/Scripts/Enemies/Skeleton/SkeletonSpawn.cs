using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class SkeletonSpawn : MonoBehaviour
{
    public List<GameObject> skeletonsList;
    public SkeletonManager skeletonManager;

    [SerializeField]
    protected GameObject skeletonGameObject;

    private void Awake()
    {
        skeletonManager = GetComponent<SkeletonManager>();
    }

    private void Start()
    {
        SpawnPositionSkeleton();
    }

    private void SpawnPositionSkeleton()
    {
        if (skeletonManager.skeletonSpawnPoint.Count <= 0)
            return;

        var holder = skeletonGameObject.transform.Find("Holder");

        foreach (var skeletonSpawnPoint in skeletonManager.skeletonSpawnPoint)
        {
            var skeletonInstance = Instantiate(
                skeletonManager.skeletonPrefab,
                skeletonSpawnPoint,
                Quaternion.identity
            );
            skeletonInstance.name = "Skeleton_" + skeletonSpawnPoint.ToString();
            skeletonInstance.transform.parent = holder.transform;
            skeletonsList.Add(skeletonInstance);
        }
    }
}
