using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardDieBulletMod : MonoBehaviour, IBulletModifier
{
    public int Up;
    public int Down;
    public int Left;
    public int Right;
    public int Forward;
    public int Back;


    public int ModifyBulletDamage(int currentDamage)
    {
        if (IsFaceUp(transform.up)) return currentDamage + Up;
        if (IsFaceUp(-transform.up)) return currentDamage + Down;
        if (IsFaceUp(transform.right)) return currentDamage + Right;
        if (IsFaceUp(-transform.right)) return currentDamage + Left;
        if (IsFaceUp(transform.forward)) return currentDamage + Forward;
        if (IsFaceUp(-transform.forward)) return currentDamage + Back;
        return currentDamage;
    }

    public bool IsFaceUp(Vector3 direction)
    {
        return Mathf.Abs(Vector3.Angle(direction, Vector3.up)) < 0.1f;
    }

}
