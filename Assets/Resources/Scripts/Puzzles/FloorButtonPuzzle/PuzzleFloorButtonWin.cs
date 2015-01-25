using UnityEngine;
using System.Collections;
using Magicolo;

public class PuzzleFloorButtonWin : State {

	PuzzleFloorButton Layer {
		get { return ((PuzzleFloorButton)layer); }
	}
	
	public override void OnEnter() {
		//RoomFlowManager.instance.goToNextRoom();
	}
}
