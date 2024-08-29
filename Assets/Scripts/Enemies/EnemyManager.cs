using UnityEngine;

namespace Enemies
{
  public class EnemyManager : MonoBehaviour
  {
    public GameObject skeletonPrefab;
    public GameObject goblinPrefab;
    public GameObject flyingEyePrefab;
    public Vector2[] goblinSpawnPoint;
    public Vector2[] skeletonSpawnPoint;
    public Vector2[] flyingEyeSpawnPoint;
    public float spawnDelay = 0f;

    public static EnemyManager Instance { get; private set; }

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
}
