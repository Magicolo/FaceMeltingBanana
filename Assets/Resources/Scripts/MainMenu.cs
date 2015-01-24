using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// TODO:  Coder la logique du UI
	// Le joueur choisi s'il est un aventurier ou le cartographe
	// Si aventurier, on set isServer a true sinon on le met a false





	// Use this for initialization
	void Start () {
	
	

	}

	void OnGUI(){

	}


	void playerChooseAdventurer(){
		PlayerPrefs.SetString("isServer", "true");
	}
	void playerChooseCartographer(){
		PlayerPrefs.SetString("isServer", "false");
	}
		
	void Update () {

	}




}
