using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class DoorClose : State {

	Door Layer {
		get { return ((Door)layer); }
	}
	
	public override void OnUpdate() {
		Layer.cube.transform.SetLocalPosition(Mathf.Lerp(Layer.cube.transform.localPosition.y, 2, Time.deltaTime * Layer.speed), "Y");
	}
}

