using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingManager : MonoBehaviour
{
    //This enforces a singleton pattern
    public static TypingManager Instance {get; private set; }

	private void Awake () {
        //Initiate singleton
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

    /*
        This will be the manager for typing
        It will hold the dictionary for all the words
        This will track all current words being used
        It will track the current word being attempted
        It will track the current word typed
            and IF there is a mistake
        This will have the function for getting a new word
            New starting words must have a different first letter from the rest of the active words
            
    */

    List<List<string>> allWords = new List<List<string>>() {
        new List<string> {"apart", "ardor"},
        new List<string> {"balloon", "bounce"}
    };
    

    //Contains all the words currently associated with a balloon
    public List<string> activeWords;
    //Current word being attempted
    public string currentWord;
    //What has been typed so far
    public string typedWord;


    void Start() {
        allWords.ForEach(item => {
            item.ForEach(innerItem => Debug.Log(innerItem));
        });
    }

    void Update()
    {
        foreach(char letter in Input.inputString){
            Debug.Log(letter);
        }
    }

    public void getNewBalloonWord(){

    }
}
