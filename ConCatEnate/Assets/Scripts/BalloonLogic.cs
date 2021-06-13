using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;


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
    public bool isMoving = false;

    //Used for floating animation //////////////////////////////
    public float zAmplitude = 0.25f;
    public float zFrequency = 0.25f;
    public float xAmplitude = 0.01f;
    public float xFrequency = 1f;
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();

    //Used for changing color /////////////////////////////////
    Color lerpedColor = Color.blue;
    Renderer balloonRenderer;    
    float t = 0;

    //Used for word ///////////////////////////////////////////
    bool isCurrentWord = false;
    public string balloonWord = "";
    public TextMeshPro textmeshPro;



    void Start() {
        posOffset = transform.position;
        balloonRenderer = gameObject.GetComponent<Renderer>();
        lerpedColor = Color.red;
        balloonRenderer.material.SetColor("_Color", Color.red);
        balloonWord = TypingManager.Instance.getNewBalloonWord();
        // Debug.Log(balloonWord);
    }
    void changeChargeStatus(){
        chargedBalloon = false;
        mSpeed = 0.0f;
        isMoving = false;
        posOffset = transform.position;

        //set new word
        balloonWord = TypingManager.Instance.getNewBalloonWord();
    }

    // void OnTriggerEnter(Collider collider){
    //     if (collider.gameObject.tag == "cat" && chargedBalloon){
    //         isMoving = true;
    //     }
    // }


    IEnumerator chargeCountdown(int seconds) {
        int counter = seconds;
        while (counter > 0){
        yield return new WaitForSeconds (1);
            counter--;
        }
        changeChargeStatus();
    }

    // void OnCollisionEnter(Collision collision){
    //     Debug.Log("collision");
    //     //if charged become cat and set parent
    //     //else pop
    // }

    void Update() {
        setText();
        checkWord();

        if (chargedBalloon && !GameManager.Instance.stayCharged && gameObject.tag != "cat"){
            changeColor();        
        }

        // if (isMoving && chargedBalloon){
        //     float step = mSpeed * Time.deltaTime;
        //     transform.position = Vector3.MoveTowards(transform.position, mTarget.position, step);
        // } else 
        if(gameObject.tag != "cat") {
            tempPos = posOffset;
            tempPos.z += Mathf.Sin (Time.fixedTime * Mathf.PI * zFrequency) * zAmplitude;
            tempPos.x += Mathf.Sin (Time.fixedTime * Mathf.PI * xFrequency) * xAmplitude;
            transform.position = tempPos;
        }
    }

    void changeColor(){
        t += Time.deltaTime / (float)GameManager.Instance.chargeDuration;
        lerpedColor = Color.Lerp(Color.blue, Color.red, t);
        balloonRenderer.material.SetColor("_Color", lerpedColor);
    }

    void setText(){
        if(isCurrentWord) {
            // bold/color what is already typed
            string remainder = balloonWord.Substring(TypingManager.Instance.typedWord.Length);
            string styledWord = "<b><color=red>"+TypingManager.Instance.typedWord+"</color></b>"+remainder;
            textmeshPro.SetText(styledWord);
        } else {
            textmeshPro.SetText(balloonWord);
        }
    }

    void checkWord(){
        if(gameObject.tag == "cat"){
            balloonWord = "";
        } else if(isCurrentWord){
            if(TypingManager.Instance.successfulWord){
                isCurrentWord = false;
                t = 0; 
                balloonWord = "";
                balloonRenderer.material.SetColor("_Color", Color.blue);
                chargedBalloon = true;
                TypingManager.Instance.successfulWord = false;
                TypingManager.Instance.resetWord();
                if(!GameManager.Instance.stayCharged) {
                    StartCoroutine(chargeCountdown(GameManager.Instance.chargeDuration));
                }
                
            } else if(TypingManager.Instance.failedWord) {
                isCurrentWord = false;
                TypingManager.Instance.failedWord = false;
                TypingManager.Instance.resetWord();
                GameManager.Instance.balloonsLeft --;
                Destroy(gameObject);
            }
        } else if(TypingManager.Instance.currentWord == balloonWord){
            isCurrentWord = true;
        }
    }
}