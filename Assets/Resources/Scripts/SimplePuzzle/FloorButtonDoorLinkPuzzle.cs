using UnityEngine;
using System.Collections;

public class FloorButtonDoorLinkPuzzle : SimplePuzzle {

	protected override void puzzleInit(){
		FloorButton[] floorButtons = transform.parent.GetComponentsInChildren<FloorButton>();
		foreach (var button in floorButtons) {
			button.simplePuzzle = this;
		}
	}
	
	public override void handleMessage(string source, string message) {
		if(!puzzleActive) return;
		if(source == "btn"){
			foreach (var door in this.doors) {
				if(door.keyword == message){
					door.Open();
				}
			}
		}
	}
}
