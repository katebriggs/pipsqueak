using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable]
public class EnemySpawn {
    public Enemy prefab;
    public int turnCount;
    public Transform pos;
}

public class EnemyController : MonoBehaviour
{
    public float RedLightGreenLightTime = 2;
    public EnemySpawn[] enemySpawns;
    public Transform TableFloor;
    
    public static EnemyController Instance { get; private set; }

    public Enemy[] allEnemyPrefabs;

    private int currentEnemy;
    private int turnIndex = 0;
    
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SpawnEnemy(0);
        currentEnemy = 1;
    }

    public void DoEnemyPhase()
    {
        StartCoroutine(EnemyPhaseCoroutine());
    }

    IEnumerator EnemyPhaseCoroutine()
    {
        yield return new WaitForEndOfFrame();
        
        turnIndex++;

        int enemyCount = FindObjectsOfType<Enemy>().Length;
        bool hasSpawned = false;

        while (true) {
            while (currentEnemy < enemySpawns.Length && turnIndex >= enemySpawns[currentEnemy].turnCount) {
                SpawnEnemy(currentEnemy);
                currentEnemy++;
                hasSpawned = true;
            }

            if (hasSpawned || enemyCount > 0) break;
            
            // If we have not spawned any enemies and none existed at the start of the turn, skip to the next wave.
            turnIndex++;
        }
        
        
        var enemies = FindObjectsOfType<MoveTowardPlayer>();
        foreach(var enemy in enemies)
        {
            enemy.SetEnabled(true);
        }
        yield return new WaitForSeconds(RedLightGreenLightTime);

        foreach(var enemy in enemies)
        {
            if (enemy) enemy.SetEnabled(false);
        }
        FindObjectOfType<CombatManager>().EndState(CombatStateType.EnemyApproach);
    }

    void SpawnEnemy(int index)
    {
        bool foundSpawnPos; 
        do {
            Transform targetPos = enemySpawns[index].pos;
            if (!targetPos) targetPos = transform;

            foundSpawnPos = NavMesh.SamplePosition(targetPos.position, out var hit, 2, NavMesh.AllAreas);
            if (foundSpawnPos)
            {
                Instantiate(enemySpawns[index].prefab, hit.position + Vector3.up, Quaternion.identity);
            }
        } while (!foundSpawnPos);
    }
}
