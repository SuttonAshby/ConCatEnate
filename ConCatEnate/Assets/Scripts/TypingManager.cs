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
        new List<string> {"air", "amalgamate", "append"},
        new List<string> {"balloon", "bounce", "blend"},
        new List<string> {"cuddle", "curious", "coalesce"},
        new List<string> {"dewclaw", "deflate", "danger"},
        new List<string> {"entwine", "expand", "energetic"},
        new List<string> {"fuse", "feline", "fur"},
        new List<string> {"groom", "glob", "grumpy"},
        new List<string> {"hairball", "hiss", "helium"},
        new List<string> {"inflate", "integrate", "intelligent"},
        new List<string> {"jump", "join", "jingle"},
        new List<string> {"kitty", "korat"},
        new List<string> {"link", "lithe", "litter"},
        new List<string> {"meow", "mouse", "merge"},
        new List<string> {"nap", "nibble", "nebelung"},
        new List<string> {"oxygen", "ocicat", "ocelot"},
        new List<string> {"purr", "paw", "pounce"},
        new List<string> {"quiet", "quick", "quadruped"},
        new List<string> {"rise", "run", "ragdoll"},
        new List<string> {"splice", "stealthy", "siamese"},
        new List<string> {"together", "tail", "treat"},
        new List<string> {"unite"},
        //new List<string> {"v", "v", "v"},
        new List<string> {"weld", "whisker", "wild"},
        //new List<string> {"x", "x", "x"},
        new List<string> {"yarn"},
        new List<string> {"zoomies"},
    };


    //Contains all the words currently associated with a balloon
    public List<string> activeWords;
    //Current word being attempted
    public string currentWord;
    public int currentLettersTyped = 0;
    //What has been typed so far
    public string typedWord;

    public bool successfulWord = false;
    public bool failedWord = false;


    void Start() {
        // allWords.ForEach(item => {
        //     item.ForEach(innerItem => Debug.Log(innerItem));
        // });

        // currentWord = "coquette";
        currentWord = null;
        // activeWords.AddRange(new List<string>(){"coquette", "bounce", "ardor"});
        // getNewBalloonWord();
    }

    void Update()
    {
        // getNewBalloonWord();
        
        if(currentWord == null) {
            //check for letter and then compare to activeWords
            //if there is an a matching active word set it
            foreach(string word in activeWords) {
                foreach(char letter in Input.inputString) {
                    if(word[0] == letter){
                        currentWord = word;
                        // Debug.Log("new word is: " + currentWord);
                        typedWord += letter;
                        currentLettersTyped += 1;
                    }
                }
            }


        } else {
            //This gets every single letter that was typed this frame
            foreach(char letter in Input.inputString){
                if(currentWord[currentLettersTyped] == letter){
                    typedWord += letter;
                    currentLettersTyped += 1;
                    //check if complete
                    if(currentWord == typedWord) {
                        resetWord();
                        // Debug.Log("DONE");
                        successfulWord = true;
                        
                    }
                    // Debug.Log(typedWord);
                } else {
                    resetWord();
                    // Debug.Log("POP");
                    failedWord = true;
                }
            }
        }
    }

    public string getNewBalloonWord(){
        //get first letters in activeWords
        if(activeWords.Count < allWords.Count) {
            string startingLetters = "";
            foreach(string word in activeWords){
                startingLetters += word[0];
            }

            //get random number between 0 and allWords.length
            int rowToTry = Random.Range(0, allWords.Count);
            // Debug.Log(rowToTry);
            // Debug.Log(allWords[rowToTry][0][0]);

            //check if first letter in row is in activeWords
            if(startingLetters.Contains(allWords[rowToTry][0][0].ToString())) {
                //IF TRUE call function again
                return getNewBalloonWord();
            } else {
                //IF FALSE: get random word in row
                int randomRowIndex = Random.Range(0, allWords[rowToTry].Count);
                string newWord = allWords[rowToTry][randomRowIndex];
                activeWords.Add(newWord);
                // Debug.Log(newWord);
                return newWord;
            }
        } else {
            Debug.Log("Error: There is a word from every letter");
            return string.Empty;
        }
    }

    void resetWord(){
        int currentIndex = activeWords.IndexOf(currentWord);
        activeWords.RemoveAt(currentIndex);
        currentWord = null;
        typedWord = null;
        currentLettersTyped = 0;
    }
}
