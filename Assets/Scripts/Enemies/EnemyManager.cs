using UnityEngine;

namespace Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public GoblinSpawn goblinSpawn;
        public GoblinManager goblinManager;

        public static EnemyManager Instance { get; private set; }

        protected void Awake()
        {
            goblinSpawn = GetComponent<GoblinSpawn>();
            goblinManager = GetComponent<GoblinManager>();

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
