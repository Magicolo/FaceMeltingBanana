using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class KeySnakeIdle : State {

	KeySnake Layer {
		get { return ((KeySnake)layer); }
	}
	
	public override void TriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			SwitchState<KeySnakePickedUp>();
		}
	}
}

