using UnityEngine;

namespace Enemies
{
   public class GoblinSpawn : MonoBehaviour
   {
      private void Start()
      {
         SpawnPositionGoblin();
      }

      private void SpawnPositionGoblin()
      {
         if (EnemyManager.Instance.goblinSpawnPoint.Length <= 0) return;
      
         foreach (var goblinSpawnPoint in EnemyManager.Instance.goblinSpawnPoint)
         {
            Instantiate(EnemyManager.Instance.goblinPrefab, goblinSpawnPoint, Quaternion.identity);      
         }
      }
   }
}
