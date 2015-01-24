using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnakePlayerOn : State {

    public override void TriggerExit(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<TileSnakeFalling>();
			GetComponentInChildren<MeshRenderer>().material.color = Color.red;
		}
	}
}

