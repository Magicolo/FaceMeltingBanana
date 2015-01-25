using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class RiddleItemIdle : State {

	RiddleItem Layer {
		get { return ((RiddleItem)layer); }
	}
    
	public override void OnEnter() {
		if (Layer.debug) {
			GetComponentInChildren<MeshRenderer>().material.color = Layer.isCorrectChoice ? Color.green : Color.red;
		}
	}
	
	public override void TriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			SwitchState<RiddleItemPickedUp>();
		}
	}
}

