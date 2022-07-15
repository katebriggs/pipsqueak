using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCannon : MonoBehaviour
{
    public Rigidbody DicePrefab;
    public int fireForce;
    public int spinForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotateCannonAmount = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            var dice = Instantiate(DicePrefab, transform.position, Quaternion.identity);
            dice.AddForce(GetFireDirection() * fireForce);
            dice.AddTorque(Random.onUnitSphere * spinForce);
        }
    }

    Vector3 GetFireDirection()
    {
        return -transform.up;
    }
}
