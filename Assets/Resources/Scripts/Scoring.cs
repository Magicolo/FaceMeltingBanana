using UnityEngine;
using System.Collections;

public class Scoring : MonoBehaviour {

	public int score = 30;

	public void levelWin(){
		score +=10;

	}

	public void levelFail(){
		score -=10;

	}

	public void checkWinLoseCondition(){

		//If we fail
		if (score<=0){
			//load fail scene
			Application.LoadLevel ("WinScene");

		} else if (score>=100){
			Application.LoadLevel ("LoseScene");
			//win
			//load win scene

		}

	}




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
