using UnityEngine;
using System.Collections;
using Magicolo;

public class FloorButtonIdle : State {

	FloorButton Layer {
    	get { return ((FloorButton)layer); }
    }
    
	public override void OnEnter() {
		
	}
	
	public override void TriggerEnter(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<FloorButtonPlayerOn>();
		}
	}
}
