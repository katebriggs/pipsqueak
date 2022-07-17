using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulatableSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] ModulatableSound selfPrefab;

    public void Play(AudioClip clip, float pitch)
    {
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.Play();
        Invoke(nameof(CommitHonorableSudokuAtTheBehestOfOnesFeudalLord), audioSource.clip.length + 1);
    }

    public void PlayScale(AudioClip clip, float[] pitches, float delay)
    {
        StartCoroutine(PlayScaleCoroutine(clip, pitches, delay));
    }

    private IEnumerator PlayScaleCoroutine(AudioClip clip, float[] pitches, float delay)
    {
        for(int i = 0; i < pitches.Length; i++)
        {
            Instantiate(selfPrefab).Play(clip, pitches[i]);
            yield return new WaitForSeconds(delay);
        }
        CommitHonorableSudokuAtTheBehestOfOnesFeudalLord();
    }

    private void CommitHonorableSudokuAtTheBehestOfOnesFeudalLord()
    {
        Destroy(gameObject);
    }
}
