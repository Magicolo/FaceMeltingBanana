using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class PuzzleSnakeSuccess : State {

	PuzzleSnake Layer {
		get { return ((PuzzleSnake)layer); }
	}
	
	public override void OnEnter() {
//		RoomFlowManager.instance.activeState(Layer.successString);
//		RoomFlowManager.instance.goToNextRoom();
	}
}

