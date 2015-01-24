using UnityEngine;
using System.Collections;
using Magicolo;

public class PuzzleFloorButtonPlay : State {

	
	PuzzleFloorButton Layer {
		get { return ((PuzzleFloorButton)layer); }
	}
	
	public override void OnUpdate() {
		
	}
	
	
	public void registerKeyword(string key){
		if(this.GetActiveState() != this ) return;
		
		if(Layer.currentKeySquenceLink.keyword == key){
			nextKeySequence();
		}else{
			reset();
		}
		
	}

	void nextKeySequence(){
		KeySquenceLink current = Layer.currentKeySquenceLink;
		if(current.next == null){
			Layer.OpenDoors();
			this.SwitchState<PuzzleFloorButtonWin>();
		}else{
			Layer.currentKeySquenceLink = current.next;
		}
	}

	void reset(){
		Layer.currentKeySquenceLink = Layer.firstKeySquenceLink;
	}
}
