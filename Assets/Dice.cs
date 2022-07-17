using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    float timeSpentIdle;
    System.Lazy<Camera> mainCam = new System.Lazy<Camera>(() => Camera.main);
    public GameObject glow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        glow.SetActive(false);
        if (rb.isKinematic) {
            StartCoroutine(SnapToGrid());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!rb.isKinematic) WaitForIdle();
    }

    private void WaitForIdle()
    {
        if (rb.velocity.magnitude < 0.01f)
        {
            timeSpentIdle += Time.deltaTime;
            if (timeSpentIdle > 0.1f)
            {
                rb.isKinematic = true; 
                StartCoroutine(SnapToGrid());
            }
        }
        else if (transform.position.y < -1)
        {
            rb.isKinematic = true;
            StartCoroutine(SnapToGrid());
        }
        else
        {
            timeSpentIdle = 0;
        }
    }

    private Vector3 RoundToNearest(Vector3 position)
    {
        return new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
    }

    private IEnumerator SnapToGrid()
    {
        Vector3 targetPosition = RoundToNearest(transform.position);
        var miniAngles = transform.eulerAngles / 90f;
        Quaternion targetRotation = Quaternion.Euler(RoundToNearest(miniAngles) * 90);

        if(targetPosition.y > 0.6f || targetPosition.y < 0)
        {
            float timer = 0;
            Vector3 returnPosition = mainCam.Value.transform.position - Vector3.up * 10;
            while (timer < 1)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(targetPosition, returnPosition, timer);
                yield return null;
            }

            //FindObjectOfType<CombatManager>().LastState(CombatStateType.LetTheDiceSettle);
            Destroy(gameObject);
            yield break;
        }


        while (Vector3.Distance(transform.position, targetPosition) > 0.001f
            || Mathf.Abs(Quaternion.Angle(transform.rotation, targetRotation)) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 4);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360);
            yield return new WaitForFixedUpdate();
        }

        FindObjectOfType<CombatManager>().EndState(CombatStateType.LetTheDiceSettle);
        
        glow.SetActive(true);
        glow.transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
    }
}
