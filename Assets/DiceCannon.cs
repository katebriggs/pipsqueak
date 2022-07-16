using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceCannon : MonoBehaviour
{
    public Rigidbody DicePrefab;
    public int fireForce;
    public int spinForce;
    public float diceDelay = 0.5f;

    private Animator _animator;

    System.Lazy<Camera> mainCam = new System.Lazy<Camera>(() => Camera.main);

    private bool _isCreatingDice = false;
    private static readonly int Throw = Animator.StringToHash("Throw");

    // Start is called before the first frame update
    void Start() {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (_isCreatingDice) return;
        var mouseRay = mainCam.Value.ScreenPointToRay(Input.mousePosition);
        var targetPoint = mouseRay.GetPoint((transform.position.y - mouseRay.origin.y) / mouseRay.direction.y);

        transform.rotation = Quaternion.LookRotation(targetPoint - transform.position);

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) {
            _isCreatingDice = true;
            _animator.SetTrigger(Throw);
            Invoke(nameof(CreateDice), diceDelay);
        }
    }

    private void CreateDice() {
        _isCreatingDice = false;
        var dice = Instantiate(DicePrefab, transform.position, Quaternion.identity);
        dice.AddForce(GetFireDirection() * fireForce);
        dice.AddTorque(Random.onUnitSphere * spinForce);

        FindObjectOfType<CombatManager>().EndState(CombatStateType.RollTheDice);
    }

    Vector3 GetFireDirection()
    {
        return transform.forward;
    }
}
