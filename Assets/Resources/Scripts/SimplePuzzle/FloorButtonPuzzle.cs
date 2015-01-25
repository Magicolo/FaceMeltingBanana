using UnityEngine;
using System.Collections;

public class FloorButtonPuzzle : SimplePuzzle {

	public KeySquenceLink firstKeySquenceLink;
	[Disable] public KeySquenceLink currentKeySquenceLink;
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected override void puzzleInit(){
		FloorButton[] floorButtons = transform.parent.GetComponentsInChildren<FloorButton>();
		foreach (var button in floorButtons) {
			button.simplePuzzle = this;
		}
	}
	
	public override void handleMessage(string source, string message) {
		if(!puzzleActive) return;
		if(source == "btn"){
			if(currentKeySquenceLink.keyword == message){
				nextKeySequence();
			}else{
				reset();
			}
		}
	}

	void nextKeySequence(){
		KeySquenceLink current = currentKeySquenceLink;
		if(current.next == null){
			openAllDoors();
			this.win();
		}else{
			currentKeySquenceLink = current.next;
		}
	}

	void reset(){
		currentKeySquenceLink = firstKeySquenceLink;
	}
}
