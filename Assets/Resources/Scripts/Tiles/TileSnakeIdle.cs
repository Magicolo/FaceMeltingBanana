using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnakeIdle : State {

	TileSnake Layer {
		get { return ((TileSnake)layer); }
	}
	
	public override void OnEnter() {
		if (Layer.debug) {
			Layer.cubeMeshRenderer.material.color = Color.yellow;
		}
	}
	
	public override void TriggerEnter(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<TileSnakePlayerOn>();
		}
	}
}

