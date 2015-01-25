using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class DoorOpen : State {

    Door Layer {
		get { return ((Door)layer); }
	}
	
	public override void OnStart() {
		Layer.cubeMeshRenderer.material.color = new Color(0,0,0,0);
		Layer.cube.GetComponent<BoxCollider>().enabled = false;
	}
}

