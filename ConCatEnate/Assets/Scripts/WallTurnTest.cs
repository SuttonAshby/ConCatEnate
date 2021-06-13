using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTurnTest : MonoBehaviour
{
void OnControllerColliderHit ( ControllerColliderHit hit ) {
 
        if (hit.collider.gameObject.tag == "Wall"){
         
            Vector3 eulerAngles = transform.rotation.eulerAngles;          
 
            // Set the altered rotation back
            transform.rotation = Quaternion.AngleAxis(90, transform.up) * transform.rotation;       
 
        Debug.Log ("HIT WALL - ROTATING!"); // Display it in UI
        }
}
}
