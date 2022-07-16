using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed;
    public int NumBounces = 3;
    public Vector3 Direction { get; set; }

    int DamageValue = 1;

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, Direction, out var hit, Speed * Time.deltaTime))
        {
            var bulletMod = hit.collider.GetComponentInParent<IBulletModifier>();
            if(bulletMod != null){
                print($"From {DamageValue}...");

                DamageValue = bulletMod.ModifyBulletDamage(DamageValue);

                print($"To {DamageValue}!");
            }

            var bulletReceiver = hit.collider.GetComponentInParent<IBulletReceiver>();
            if(bulletReceiver != null)
            {
                bulletReceiver.TakeBulletDamage(DamageValue);
                NumBounces = 0;
            }

            NumBounces--;
            if (NumBounces < 0)
            {
                FindObjectOfType<CombatManager>().EndState(CombatStateType.FireAway);
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
}

interface IBulletModifier
{
    int ModifyBulletDamage(int currentDamage);
}

interface IBulletReceiver
{
    void TakeBulletDamage(int damage);
}