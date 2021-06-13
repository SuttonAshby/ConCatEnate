using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
public static ButtonSound Instance {get; private set; }

private void Awake () {
    //Initiate singleton
	if (Instance == null) {
		Instance = this;
		DontDestroyOnLoad(gameObject);
	} else {
		Destroy(gameObject);
	}
}
public AudioSource audioSource;
public AudioClip audioClip;
 
public void playClip(){
audioSource.clip = audioClip;
audioSource.Play();
}
}