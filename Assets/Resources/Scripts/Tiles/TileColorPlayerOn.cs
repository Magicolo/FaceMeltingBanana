using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileColorPlayerOn : State {

	TileColor Layer {
		get { return ((TileColor)layer); }
	}
    
	public override void OnEnter() {
		if (Layer.debug) {
			Layer.cubeMeshRenderer.material.color = Color.green;
		}
		
		if (Layer.isNextTile) {
			Layer.isNextTile = false;
			Layer.nextTile.isNextTile = true;
			
			if (Layer.nextTile.debug) {
				Layer.nextTile.cubeMeshRenderer.material.color = Color.cyan;
			}
		}
		else {
			SwitchState<TileColorError>();
		}
	}
	
	public override void TriggerExit(Collider collision) {
		if (collision.gameObject.name == "Player") {
			SwitchState<TileColorIdle>();
		}
	}
}

