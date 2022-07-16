using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileLauncher : MonoBehaviour
{
    public int NumBounces;
    public ReticuleController reticule;
    public PlayerBullet ProjectilePrefab;
    System.Lazy<Camera> mainCam = new System.Lazy<Camera>(() => Camera.main);

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = mainCam.Value.ScreenPointToRay(Input.mousePosition);
        var targetPoint = mouseRay.GetPoint((transform.position.y - mouseRay.origin.y) / mouseRay.direction.y);

        var inDirection = (targetPoint - transform.position).normalized;

        reticule.DrawReticule(transform.position, inDirection);

        if (Input.GetButtonDown("Jump"))
        {
            var bullet = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            bullet.Direction = (targetPoint - transform.position).normalized;

            FindObjectOfType<CombatManager>().EndState(CombatStateType.ReadySteadyAim);
        }
    }
}
