using UnityEngine;

public class Block : MonoBehaviour, IBulletModifier
{
    public int ModifyBulletDamage(int currentDamage)
    {
        Destroy(gameObject);

        return currentDamage;
    }
}