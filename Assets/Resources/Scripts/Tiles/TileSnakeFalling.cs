using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnakeFalling : State {

	TileSnake Layer {
		get { return ((TileSnake)layer); }
	}
	
	public override void OnEnter() {
		if (Layer.debug){
			Layer.cubeMeshRenderer.material.color = Color.red;
		}
		rigidbody.isKinematic = false;
		rigidbody.WakeUp();
	}
}

