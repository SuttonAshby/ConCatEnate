using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set; }

	private void Awake () {
        //Initiate singleton
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
    if (!BGM.isPlaying) {
        BGM.Play();
    }
        
    }
    

    // Update is called once per frame
    void Update() {
    }
    public void ChangeBGM(AudioClip music)  
    {
        if(BGM.clip.name == music.name)
        return;

        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
}
}