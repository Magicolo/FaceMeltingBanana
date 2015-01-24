using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class PuzzleSnakeNormal : State {

	PuzzleSnake Layer {
		get { return ((PuzzleSnake)layer); }
	}
	
	public override void OnUpdate() {
		if (Layer.player.transform.position.y < -10) {
			SwitchState<PuzzleSnakeFail>();
		}
	}
}

