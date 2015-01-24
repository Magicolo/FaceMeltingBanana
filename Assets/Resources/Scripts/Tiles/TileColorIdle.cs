using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileColorIdle : State {

	TileColor Layer {
    	get { return ((TileColor)layer); }
    }
    
	public override void OnEnter() {
		if (Layer.debug){
			Layer.cubeMeshRenderer.material.color = Layer.color;
		}
	}
	
	public override void TriggerEnter(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<TileColorPlayerOn>();
		}
	}
}

