using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float RedLightGreenLightTime = 2;

    public void DoEnemyPhase()
    {
        StartCoroutine(EnemyPhaseCoroutine());
    }

    IEnumerator EnemyPhaseCoroutine()
    {
        yield return new WaitForEndOfFrame();
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
}
