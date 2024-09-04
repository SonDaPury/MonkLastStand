using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class GoblinManager : EnemyManager
{
    public GameObject goblinPrefab;
    public List<Vector2> goblinSpawnPoint;

    private void Start() { }

    private void Reset()
    {
        goblinSpawnPoint.Add(new Vector2(36, 26.32f));
        goblinSpawnPoint.Add(new Vector2(43.52f, 18.38f));
    }
}
