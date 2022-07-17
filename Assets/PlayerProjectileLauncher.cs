using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileLauncher : MonoBehaviour
{
    public int NumBounces;
    public ReticuleController reticule;
    public PlayerBullet ProjectilePrefab;
    System.Lazy<Camera> mainCam = new System.Lazy<Camera>(() => Camera.main);
    public Transform barrel;
    public int MaxAmmo = 6;
    [HideInInspector] public int AmmoCount = 6;

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = mainCam.Value.ScreenPointToRay(Input.mousePosition);
        var targetPoint = mouseRay.GetPoint((barrel.position.y - mouseRay.origin.y) / mouseRay.direction.y);

        Vector3 lookTarget = targetPoint;
        lookTarget.y = transform.position.y;
        transform.LookAt(lookTarget, Vector3.up);

        var inDirection = (targetPoint - barrel.position).normalized;

        reticule.DrawReticule(barrel.position, inDirection);

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            var bullet = Instantiate(ProjectilePrefab, barrel.position, Quaternion.identity);
            bullet.Direction = (targetPoint - barrel.position).normalized;

            var combatManager = FindObjectOfType<CombatManager>();
            
            combatManager.EndState(CombatStateType.ReadySteadyAim);
            SetAmmoCount(AmmoCount - 1);
        }
    }

    public void Reload()
    {
        SetAmmoCount(MaxAmmo);
    }

    private void SetAmmoCount(int ammo)
    {
        AmmoCount = ammo;
        FindObjectOfType<AmmoPanel>()?.SetCount(ammo);
    }
}
