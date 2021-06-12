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
bool chargedBalloon = false;//set to false by default; would need to be changed to true in order for the balloon to do anything.
//aTimer = new System.Timers.Timer(10000);

void changeChargeStatus(){
    chargedBalloon = false;
    mSpeed = 0.0f;
}

private void OnTriggerEnter(Collider2D collision){
    chargedBalloon = true;
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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    /*
        if (Vector3.Distance(mTarget.transform.position) < 100){
            chargedBalloon = true;
            transform.LookAt(mTarget.position);
            transform.Translate(0.0f, 0.0f, mSpeed*Time.deltaTime);
            /*if (balloon object touches cat){
                balloon object sticks to cat objects
            }
            else{
                balloon object pops
            }
            
        }
        else if (Vector3.Distance(mTarget.transform.position) > 100){
            chargedBalloon = false;
        }
        }}
    */    
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (chargedBalloon == true){
            transform.LookAt(mTarget.position);
            transform.Translate(0.0f, 0.0f, mSpeed*Time.deltaTime);
            StartCoroutine(chargeCountdown(10));
            //Need an if statement for if balloon touches cat, it sticks to cat...
            //await Task.Delay(5000);
            //chargedBalloon == false;
        //}
        //else{
            //For now, telling balloon object to stop moving...
          //  mSpeed = 0.0f;
        //}
    }
  }
 }
        
    

