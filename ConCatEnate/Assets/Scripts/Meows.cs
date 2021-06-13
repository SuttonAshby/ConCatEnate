using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Meows : MonoBehaviour
{

public AudioSource _as;
private int clipIndex;
public AudioClip[] audioClipArray;

void Awake(){
    _as = GetComponent<AudioSource> ();
}

void Start () {
    _as.clip = audioClipArray[Random.Range(0,audioClipArray.Length)];
    _as.PlayOneShot (_as.clip);
    StartCoroutine(PlaySound());
}

IEnumerator PlaySound()
{
    yield return new WaitForSeconds(Random.Range(10f, 40f));

    clipIndex = Random.Range(0, audioClipArray.Length - 1);
    _as.PlayOneShot(audioClipArray[clipIndex], 1f);

    yield return new WaitForSeconds(audioClipArray[clipIndex].length);
    StartCoroutine(PlaySound());
}
}

