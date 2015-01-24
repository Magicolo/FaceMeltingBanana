using UnityEngine;
using System.Collections;
using Magicolo;

public class FloorButtonPlayerOn : State {

	FloorButton Layer {
		get { return ((FloorButton)layer); }
	}
    
	public override void OnEnter() {
		Layer.puzzle.registerKeyword(this.Layer.keyword);
	}
	
	public override void TriggerExit(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<FloorButtonIdle>();
		}
	}
}
