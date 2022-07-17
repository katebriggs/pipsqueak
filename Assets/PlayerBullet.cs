using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed;
    public int NumBounces = 3;
    public Vector3 Direction { get; set; }
    [SerializeField] ModulatableSound modulatableSoundPrefab;
    [SerializeField] AudioClip hitDieClip;

    readonly float frequencyPitchBase = Mathf.Pow(2, 1f / 12);

    int DamageValue = 0;


    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Direction, out var hit, Speed * Time.deltaTime))
        {
            var bulletMod = hit.collider.GetComponentInParent<IBulletModifier>();
            if (bulletMod != null)
            {
                DamageValue = bulletMod.ModifyBulletDamage(DamageValue);
                float pitch = CalculatePitch(3- NumBounces);
                Instantiate(modulatableSoundPrefab).Play(hitDieClip, pitch);
            }

            var bulletReceiver = hit.collider.GetComponentInParent<IBulletReceiver>();
            if (bulletReceiver != null)
            {
                int steps = 3 - NumBounces;
                float basePitch = CalculatePitch(steps);
                float subPitch = CalculatePitch(steps + 1);
                float superPitch = CalculatePitch(steps + 2);
                Instantiate(modulatableSoundPrefab).PlayScale(hitDieClip, new float[] { basePitch,subPitch,superPitch}, 0.1f);

                bulletReceiver.TakeBulletDamage(DamageValue);
                NumBounces = 0;
            }

            NumBounces--;
            if (NumBounces < 0)
            {
                var ppl = FindObjectOfType<PlayerProjectileLauncher>();
                CombatManager combatManager = FindObjectOfType<CombatManager>();
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                if (ppl.AmmoCount == 0 || enemies.Length == 0 || (enemies.Length == 1 && enemies[0] == bulletReceiver && enemies[0].HP <= 0)) {
                    combatManager.EndState(CombatStateType.FireAway);
                }
                else
                {
                    combatManager.LastState(CombatStateType.FireAway);
                }
                Destroy(gameObject);
            }
            else
            {
                var normal = hit.normal;
                normal.y = 0;
                Direction = Vector3.Reflect(Direction, normal);
            }

            transform.position = hit.point + (Direction * Speed * Time.deltaTime);
        }
        else
        {
            transform.position += Direction * Speed * Time.deltaTime;
        }


    }

    private float CalculatePitch(int steps)
    {
        return Mathf.Pow(frequencyPitchBase, steps);
    }
}

interface IBulletModifier
{
    int ModifyBulletDamage(int currentDamage);
}

interface IBulletReceiver
{
    void TakeBulletDamage(int damage);
}