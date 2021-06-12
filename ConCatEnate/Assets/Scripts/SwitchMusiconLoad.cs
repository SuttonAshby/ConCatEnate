using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusiconLoad : MonoBehaviour
{
    public AudioClip newTrack;

    private AudioManager theAM;

void Start () {
    theAM = FindObjectOfType<AudioManager>();

    if(newTrack != null)
    theAM.ChangeBGM(newTrack);
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
