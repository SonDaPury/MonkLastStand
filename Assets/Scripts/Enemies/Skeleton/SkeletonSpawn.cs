using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class SkeletonSpawn : MonoBehaviour
{
    public List<GameObject> skeletonsList;
    public SkeletonManager skeletonManager;
    public float respawnTime = 10f;
    public static SkeletonSpawn Instance { get; private set; }

    [SerializeField]
    protected GameObject skeletonGameObject;

    private void Awake()
    {
        skeletonManager = GetComponent<SkeletonManager>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("GoblinSpawn đã được khởi tạo.");
        }
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

    // Hàm xử lý khi Goblin chết
    public void OnSkeletonDeath(GameObject skeleton)
    {
        StartCoroutine(RespawnSkeleton(skeleton));
    }

    private IEnumerator RespawnSkeleton(GameObject skeleton)
    {
        yield return new WaitForSeconds(respawnTime);
        skeleton.GetComponent<SkeletonHealth>().Respawn();
    }
}
