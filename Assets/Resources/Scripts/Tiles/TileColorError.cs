using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileColorError : State {

	TileColor Layer {
    	get { return ((TileColor)layer); }
    }
    
	public override void OnEnter() {
		if (Layer.debug){
			Layer.cubeMeshRenderer.material.color = Color.red;
		}
		
		Layer.isNextTile = false;
	}
}

