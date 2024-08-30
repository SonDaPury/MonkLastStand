using UnityEngine;

namespace Enemies
{
    public class GoblinSpawn : MonoBehaviour
    {
        [SerializeField]
        protected GameObject goblinGameObject;

        private void Start()
        {
            SpawnPositionGoblin();
        }

        private void SpawnPositionGoblin()
        {
            if (EnemyManager.Instance.goblinSpawnPoint.Length <= 0)
                return;

            var holder = goblinGameObject.transform.Find("Holder");

            foreach (var goblinSpawnPoint in EnemyManager.Instance.goblinSpawnPoint)
            {
                var golblinInstance = Instantiate(
                    EnemyManager.Instance.goblinPrefab,
                    goblinSpawnPoint,
                    Quaternion.identity
                );
                golblinInstance.name = "Goblin_" + goblinSpawnPoint.ToString();
                golblinInstance.transform.parent = holder.transform;
            }
        }
    }
}
