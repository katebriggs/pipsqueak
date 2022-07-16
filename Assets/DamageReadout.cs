using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReadout : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text TextMesh;

    [SerializeField] float floatSpeed;
    [SerializeField] float fadeTime;

    float elapsedTime;
    System.Lazy<Camera> mainCam = new System.Lazy<Camera>(() => Camera.main);

    // Start is called before the first frame update
    void Start()
    {  
        StartCoroutine(FloatAndFade());
    }

    public void SetText(int Damage)
    {
        TextMesh.text = Damage.ToString(); 
    }

    IEnumerator FloatAndFade()
    {
        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;
            TextMesh.color = Color.Lerp(Color.white, Color.clear, Mathf.InverseLerp(0, fadeTime, elapsedTime));

            transform.rotation = Quaternion.LookRotation(transform.position - mainCam.Value.transform.position, Vector3.forward);

            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
