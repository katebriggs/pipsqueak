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
    bool isFireHeld;
    float holdTime;

    [SerializeField]Transform handTransform;
    Vector3 initialHandPosition;
    [SerializeField] AudioSource diceShakeSource;
    [SerializeField] AudioSource diceRollSource;

    System.Lazy<Camera> mainCam = new System.Lazy<Camera>(() => Camera.main);

    private bool _isCreatingDice = false;
    private static readonly int Throw = Animator.StringToHash("Throw");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        initialHandPosition = handTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCreatingDice) return;
        var mouseRay = mainCam.Value.ScreenPointToRay(Input.mousePosition);
        var targetPoint = mouseRay.GetPoint((transform.position.y - mouseRay.origin.y) / mouseRay.direction.y);

        transform.rotation = Quaternion.LookRotation(targetPoint - transform.position);

        if(!_isCreatingDice && !isFireHeld && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")))
        {
            isFireHeld = true;
            holdTime = 0;
            diceShakeSource.Play();
        }

        if (isFireHeld)
        {
            handTransform.position = initialHandPosition + (Random.onUnitSphere * 0.1f);
            holdTime += Time.deltaTime;
        }

        if (isFireHeld && (Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1") || holdTime >= 1))
        {
            isFireHeld = false;
            handTransform.position = initialHandPosition;
            diceShakeSource.Stop();
            diceRollSource.Play();

            _isCreatingDice = true;
            _animator.SetTrigger(Throw);
            Invoke(nameof(CreateDice), diceDelay);
            Invoke(nameof(CreateDice), diceDelay + 0.1f);
            Invoke(nameof(CreateDice), diceDelay + 0.2f);
        }
    }

    private void CreateDice()
    {
        _isCreatingDice = false;
        var dice = Instantiate(DicePrefab, transform.position, Quaternion.identity);
        dice.AddForce(GetFireDirection() * fireForce * Mathf.Lerp(0.5f,1.1f, holdTime));
        dice.AddTorque(Random.onUnitSphere * spinForce);

        FindObjectOfType<CombatManager>().EndState(CombatStateType.RollTheDice);
    }

    Vector3 GetFireDirection()
    {
        return transform.forward + Random.onUnitSphere / 10f;
    }
}
