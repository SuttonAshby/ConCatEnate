using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingAnimation : MonoBehaviour
{

    //Worked off of
    // Floater v0.0.2
    // by Donovan Keith
    //
    // [MIT License](https://opensource.org/licenses/MIT)
    public float zAmplitude = 0.25f;
    public float zFrequency = 0.25f;
    public float xAmplitude = 0.01f;
    public float xFrequency = 1f;
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
     
    // Update is called once per frame
    void Update () {
        
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.z += Mathf.Sin (Time.fixedTime * Mathf.PI * zFrequency) * zAmplitude;
        tempPos.x += Mathf.Sin (Time.fixedTime * Mathf.PI * xFrequency) * xAmplitude;
        transform.position = tempPos;
    }
}
