using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceCannon : MonoBehaviour
{
    public Rigidbody DicePrefab;
    public int fireForce;
    public int spinForce;

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

        transform.rotation = Quaternion.LookRotation(targetPoint - transform.position);

        if (Input.GetButtonDown("Jump"))
        {
            var dice = Instantiate(DicePrefab, transform.position, Quaternion.identity);
            dice.AddForce(GetFireDirection() * fireForce);
            dice.AddTorque(Random.onUnitSphere * spinForce);

            FindObjectOfType<CombatManager>().EndState(CombatStateType.RollTheDice);
        }
    }

    Vector3 GetFireDirection()
    {
        return transform.forward;
    }
}
