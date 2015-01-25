using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnakePlayerOn : State {

	public float timeBeforeFall = 5;
	public float trembleStrength = 5;
	
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
		
		rigidbody.MoveRotation(Quaternion.Euler(Random.Range(-trembleStrength, trembleStrength), Random.Range(-trembleStrength, trembleStrength), Random.Range(-trembleStrength, trembleStrength)));
	}
	
	public override void TriggerExit(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			SwitchState<TileSnakeFalling>();
		}
	}
}

