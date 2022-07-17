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
    public int corner;
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

        if (currentEnemy < enemySpawns.Length && turnIndex >= enemySpawns[currentEnemy].turnCount) {
            SpawnEnemy(currentEnemy);
            currentEnemy++;
        }
        
        var enemies = FindObjectsOfType<MoveTowardPlayer>();
        foreach(var enemy in enemies)
        {
            enemy.SetEnabled(true);
        }
        yield return new WaitForSeconds(RedLightGreenLightTime);

        foreach(var enemy in enemies)
        {
            enemy.SetEnabled(false);
        }
        FindObjectOfType<CombatManager>().EndState(CombatStateType.EnemyApproach);

        turnIndex++;
    }

    void SpawnEnemy(int index)
    {
        bool foundSpawnPos; 
        do
        {
            var angleOffset = enemySpawns[index].corner * 90;
            var targetPosition = Quaternion.Euler(0, angleOffset, 0) * new Vector3(9, 0, 9);

            foundSpawnPos = NavMesh.SamplePosition(TableFloor.position + targetPosition, out var hit, 2, NavMesh.AllAreas);
            if (foundSpawnPos)
            {
                Instantiate(enemySpawns[index].prefab, hit.position + Vector3.up, Quaternion.identity);
            }
        } while (!foundSpawnPos);
    }
}
