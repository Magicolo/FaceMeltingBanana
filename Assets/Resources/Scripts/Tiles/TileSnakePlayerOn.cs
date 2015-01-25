using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnakePlayerOn : State {

	public float timeBeforeFall = 5;
	
	TileSnake Layer {
		get { return ((TileSnake)layer); }
	}
	
	public override void OnEnter() {
		if (Layer.debug) {
			Layer.cubeMeshRenderer.material.color = Color.green;
		}
	}
	
	public override void OnUpdate() {
		timeBeforeFall -= Time.deltaTime;
		
		if (timeBeforeFall <= 0) {
			SwitchState<TileSnakeFalling>();
		}
	}
	
	public override void TriggerExit(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<TileSnakeFalling>();
		}
	}
}

