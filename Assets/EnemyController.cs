using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float RedLightGreenLightTime = 2;
    public NavMeshAgent EnemyPrefab;
    public Transform TableFloor;

    public void DoEnemyPhase()
    {
        StartCoroutine(EnemyPhaseCoroutine());
    }

    IEnumerator EnemyPhaseCoroutine()
    {
        yield return new WaitForEndOfFrame();
        SpawnEnemy();
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
    }

    void SpawnEnemy()
    {

        bool foundSpawnPos; 
        do
        {
            var angleOffset = Random.Range(0, 4) * 90;
            var axisOffset = Random.Range(-9, 9);
            var targetPosition = Quaternion.Euler(0, angleOffset, 0) * new Vector3(axisOffset, 0, 10);

            foundSpawnPos = NavMesh.SamplePosition(TableFloor.position + targetPosition, out var hit, 2, NavMesh.AllAreas);
            if (foundSpawnPos)
            {
                Instantiate(EnemyPrefab, hit.position + Vector3.up, Quaternion.identity);
            }
        } while (!foundSpawnPos);
    }
}
