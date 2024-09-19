using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class GoblinSpawn : MonoBehaviour
    {
        public List<GameObject> goblinsList;
        public float respawnTime = 10f;
        public static GoblinSpawn Instance { get; private set; }

        [SerializeField]
        protected GameObject goblinGameObject;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SpawnPositionGoblin();
        }

        private void SpawnPositionGoblin()
        {
            if (EnemyManager.Instance.goblinManager.goblinSpawnPoint.Count <= 0)
                return;

            var holder = goblinGameObject.transform.Find("Holder");

            foreach (var goblinSpawnPoint in EnemyManager.Instance.goblinManager.goblinSpawnPoint)
            {
                var golblinInstance = Instantiate(
                    EnemyManager.Instance.goblinManager.goblinPrefab,
                    goblinSpawnPoint,
                    Quaternion.identity
                );
                golblinInstance.name = "Goblin_" + goblinSpawnPoint.ToString();
                golblinInstance.transform.parent = holder.transform;
                goblinsList.Add(golblinInstance);
            }
        }

        // Hàm xử lý khi Goblin chết
        public void OnGoblinDeath(GameObject goblin)
        {
            StartCoroutine(RespawnGoblin(goblin));
        }

        private IEnumerator RespawnGoblin(GameObject goblin)
        {
            yield return new WaitForSeconds(respawnTime);
            goblin.GetComponent<GoblinHealth>().Respawn();
        }
    }
}
