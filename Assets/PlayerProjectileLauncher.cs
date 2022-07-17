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

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = mainCam.Value.ScreenPointToRay(Input.mousePosition);
        var targetPoint = mouseRay.GetPoint((barrel.position.y - mouseRay.origin.y) / mouseRay.direction.y);
        
        transform.LookAt(targetPoint, Vector3.up);

        var inDirection = (targetPoint - barrel.position).normalized;

        reticule.DrawReticule(barrel.position, inDirection);

        if (Input.GetButtonDown("Jump"))
        {
            var bullet = Instantiate(ProjectilePrefab, barrel.position, Quaternion.identity);
            bullet.Direction = (targetPoint - barrel.position).normalized;

            FindObjectOfType<CombatManager>().EndState(CombatStateType.ReadySteadyAim);
        }
    }
}
