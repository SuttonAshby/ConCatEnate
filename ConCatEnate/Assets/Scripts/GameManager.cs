using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //This enforces a singleton pattern
    public static GameManager Instance {get; private set; }

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
    This will track general game information
        win/lose state: number of balloons stuck to cat
    */

    public int balloonsLeft = 20;
    public int balloonsNeeded = 15;
    public int balloonsAttached = 0;

    public int chargeDuration = 10;
    
    //FOR DEBUGGING
    public bool stayCharged = false;

    void resetGame(){
        balloonsLeft = 20;
        balloonsNeeded = 10;
        balloonsAttached = 0;
        TypingManager.Instance.activeWords.Clear();
        TypingManager.Instance.resetWord();
    }

    // Start is called before the first frame update
    void Update()
    {
        // checkWinState();
    }

    public void checkWinState() {
        if(balloonsAttached == balloonsNeeded) {
            //Win State
            SceneManager.LoadScene("Win");
            resetGame();
        } else if (balloonsLeft + balloonsAttached < balloonsNeeded) {
            //Lose State
            SceneManager.LoadScene("Lose");
            resetGame();
        }
    }
}
