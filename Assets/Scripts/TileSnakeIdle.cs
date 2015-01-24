using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnakeIdle : State {

	public override void TriggerEnter(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<TileSnakePlayerOn>();
			GetComponentInChildren<MeshRenderer>().material.color = Color.green;
		}
	}
}

