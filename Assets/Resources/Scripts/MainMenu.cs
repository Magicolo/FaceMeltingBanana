using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// TODO:  Coder la logique du UI
	// Le joueur choisi s'il est un aventurier ou le cartographe
	// Si aventurier, on set isServer a true sinon on le met a false


	public string levelName;


	// Use this for initialization
	void Start () {
	
	

	}

	void OnGUI(){

	}


	public void playerChooseAdventurer(){
		PlayerPrefs.SetInt("PlayerType", 0);
		LoadLevel();
	}
	public void playerChooseCartographer(){
		PlayerPrefs.SetInt("PlayerType", 1);
		LoadLevel();
	}
		
	void Update () {

	}
	
	void LoadLevel(){
		Application.LoadLevel(levelName);
	}




}
