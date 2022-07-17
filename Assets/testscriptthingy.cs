using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscriptthingy : MonoBehaviour
{

    public ModulatableSound ModulatableSoundPrefab;
    public AudioClip clip;
    public int[] notes;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayChord());
    }

    IEnumerator PlayChord()
    {
        foreach(var i in notes)
        {
            Instantiate(ModulatableSoundPrefab).Play(clip, CalculatePitch(i));
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(PlayChord());
    }

    readonly float frequencyPitchBase = Mathf.Pow(2, 1f / 12);

    private float CalculatePitch(int steps)
    {
        return Mathf.Pow(frequencyPitchBase, steps);
    }
}
