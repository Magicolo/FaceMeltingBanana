using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnakePlayerOn : State {

	TileSnake Layer {
		get { return ((TileSnake)layer); }
	}
	
	public override void OnEnter() {
		if (Layer.debug){
			Layer.cubeMeshRenderer.material.color = Color.green;
		}
	}
	
    public override void TriggerExit(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<TileSnakeFalling>();
		}
	}
}

