﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SimplePuzzle : MonoBehaviour {

	public bool puzzleActive = true;
	[Disable] public List<Door> doors;
	[Disable] public List<FloorButton> buttons;
	

	public void init(){
		doors = new List<Door>();
		
		foreach (Door door in transform.parent.GetComponentsInChildren<Door>()) {
			doors.Add(door);
			//door.puzzle = this;
		}
		foreach (FloorButton button in transform.parent.GetComponentsInChildren<FloorButton>()) {
			button.simplePuzzle = this;
			buttons.Add(button);
		}
	}
	
	protected virtual void puzzleInit(){}
	
	protected void openAllDoors(){
		foreach (var door in doors) {
			door.Open();
		}
	}
	
	void Update () {
	
	}
	
	public virtual void handleMessage(string source, string message) {	
	}
	
	public void fail(){
		if(!puzzleActive) return;
		AudioManager.PlayLevelSource(false);
		RoomFlowManager.instance.die();
		puzzleActive = false;
	}
	
	public void win(){
		if(!puzzleActive) return;
		AudioManager.PlayLevelSource(true);
		puzzleActive = false;
	}
}
