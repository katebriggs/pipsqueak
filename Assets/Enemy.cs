using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IBulletReceiver
{
    public int HP;

    public void TakeBulletDamage(int damage) {
        if (damage <= 0) return;
        HP -= damage;

        if (HP > 0) {
            List<Enemy> enemiesToSpawn = new List<Enemy>();
            int remainingHP = HP;
            print($"Took {damage} damage. Remaining HP: {remainingHP}");

            print("===== NEW SPAWNS =====");
            foreach (Enemy prefab in EnemyController.Instance.allEnemyPrefabs) {
                while (remainingHP >= prefab.HP) {
                    enemiesToSpawn.Add(prefab);
                    remainingHP -= prefab.HP;
                    print($"Spawning {prefab.name} with {prefab.HP} hp");
                }
            }

            if (enemiesToSpawn.Count > 0) {
                float anglePer = 360.0f / enemiesToSpawn.Count;
                for (int i = 0; i < enemiesToSpawn.Count; i++) {
                    float angle = anglePer * i;
                
                    Enemy prefab = enemiesToSpawn[i];
                    Vector3 targetPosition = Quaternion.Euler(0, angle, 0) * new Vector3(1, 0, 0);

                    bool foundSpawnPos = NavMesh.SamplePosition(transform.position + targetPosition,
                                                                out var hit, 2, NavMesh.AllAreas);
                    if (foundSpawnPos) {
                        var newEnemy = Instantiate(prefab, hit.position + Vector3.up, Quaternion.identity);
                        newEnemy.GetComponent<MoveTowardPlayer>().SetEnabled(false);
                    } else {
                        print($"Failed to spawn {prefab.name}");
                    }
                }
            }
        }

        Destroy(gameObject);
    }

}
