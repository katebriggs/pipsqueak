using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IBulletReceiver
{
    public int HP;
    public TMPro.TMP_Text HPReadout;

    private void Start()
    {
        HPReadout.text = HP.ToString();
    }

    public void TakeBulletDamage(int damage)
    {
        HP -= damage;
        HPReadout.text = HP.ToString();

        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
