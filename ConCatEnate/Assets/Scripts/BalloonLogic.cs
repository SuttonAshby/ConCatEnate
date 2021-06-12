using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonLogic : MonoBehaviour
{

/*
        This script should be for each balloons
        It will track its own word
            ~It will track matching letters with current word (stylistically)
        It will track if it is charged or not
            IF CHARGED
                It will check if the cat is in its radius and move towards it
            IF NOT
                It will have a count down from charged state to uncharged
                When it becomes uncharged 
                    It will get a new word
        It will word check
            IF it matches fully, 
                it will become charged
            ELSE IF it it wrong
                It will destroy itself if the current word typed matches it and then there is a typing error
        When it touches the cat
            IF it is charged it will become stuck to the cat and become part of the "cat"
            ELSE IF it is NOT charged it will pop
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
