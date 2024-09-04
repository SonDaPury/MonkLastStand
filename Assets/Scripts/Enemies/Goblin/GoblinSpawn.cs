using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class GoblinSpawn : MonoBehaviour
    {
        public List<GameObject> goblinsList;

        [SerializeField]
        protected GameObject goblinGameObject;

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
    }
}
