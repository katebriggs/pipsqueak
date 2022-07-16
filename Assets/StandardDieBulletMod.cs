using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardDieBulletMod : MonoBehaviour, IBulletModifier
{
    public DamageReadout DamageReadoutPrefab;

    public int Up;
    public int Down;
    public int Left;
    public int Right;
    public int Forward;
    public int Back;


    public int ModifyBulletDamage(int currentDamage)
    {
        if (IsFaceUp(transform.up)) currentDamage= currentDamage + Up;
        else if (IsFaceUp(-transform.up)) currentDamage =currentDamage + Down;
        else if(IsFaceUp(transform.right)) currentDamage =currentDamage + Right;
        else if (IsFaceUp(-transform.right)) currentDamage =currentDamage + Left;
        else if (IsFaceUp(transform.forward)) currentDamage =currentDamage + Forward;
        else if (IsFaceUp(-transform.forward)) currentDamage =currentDamage + Back;

        var damageReadout = Instantiate(DamageReadoutPrefab, transform.position, Quaternion.identity);
        damageReadout.SetText(currentDamage);

        Destroy(gameObject);

        return currentDamage;
    }

    public bool IsFaceUp(Vector3 direction)
    {
        return Mathf.Abs(Vector3.Angle(direction, Vector3.up)) < 0.1f;
    }

}
