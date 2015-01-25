using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class PuzzleSnakeFail : State {

    PuzzleSnake Layer {
		get { return ((PuzzleSnake)layer); }
	}
	
	public override void OnEnter() {
		RoomFlowManager.instance.activeState(Layer.failString);
		RoomFlowManager.instance.goToNextRoom();
	}
}

