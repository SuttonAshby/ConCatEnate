using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class BalloonLogic : MonoBehaviour
{

/*
        This script should be for each balloons
        It will track its own word
            ~It will track matching letters with current word (stylistically)
*/            
/*            
        1.It will track if it is charged or not
            IF CHARGED
                It will check if the cat is in its radius and move towards it
            IF NOT
                It will have a count down from charged state to uncharged
                When it becomes uncharged 
                    It will get a new word
*/            
/*                    
        2.It will word check
            IF it matches fully, 
                it will become charged
            ELSE IF it is wrong
                It will destroy itself if the current word typed matches it and then there is a typing error
*/            
/*                
        3.When it touches the cat
            IF it is charged it will become stuck to the cat and become part of the "cat"
            ELSE IF it is NOT charged it will pop
*/

public Transform mTarget;
float mSpeed = 1.0f;
public bool chargedBalloon = false;//set to false by default; would need to be changed to true in order for the balloon to do anything.


void changeChargeStatus(){
    chargedBalloon = false;
    mSpeed = 0.0f;
}


void OnTriggerEnter(Collider collider){
    if (collider.gameObject.tag == "cat" && chargedBalloon){
        isMoving = true;
    }
}

IEnumerator chargeCountdown(int seconds) 
{
    int counter = seconds;
    while (counter > 0){
        yield return new WaitForSeconds (1);
        counter--;
    }
    changeChargeStatus();
    /*for (float ft = 1f; ft >= 0; ft -= 0.1f) 
    {
        Color c = renderer.material.color;
        c.a = ft;
        renderer.material.color = c;
        yield return null;
    }*/
}

    public Collider balloonCollider;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        balloonCollider = gameObject.GetComponent < Collider >();
    }

void OnCollisionEnter(Collision collision){

}

    // Update is called once per frame
    void Update()
    {
 
        if (chargedBalloon == true){
        //    StartCoroutine(chargeCountdown(10));
    }
        if (isMoving){
            transform.LookAt(mTarget.position);
            transform.Translate(0.0f, 0.0f, mSpeed*Time.deltaTime);
        }

  }
 }
        
    

