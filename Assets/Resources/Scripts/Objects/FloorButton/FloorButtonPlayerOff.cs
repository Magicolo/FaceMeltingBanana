using UnityEngine;
using System.Collections;
using Magicolo;

public class FloorButtonPlayerOff : State {

	FloorButton Layer {
		get { return ((FloorButton)layer); }
	}
	
	private float t;
	private Vector3 startingPosition;
	private Vector3 finalPosition;
    
	public override void OnEnter() {
		t = 0;
		startingPosition = this.transform.position;
		finalPosition = new Vector3(startingPosition.x, startingPosition.y + 0.09f, startingPosition.z);
	}
	
	public override void OnUpdate() {
		if(t<1){
			t+= Time.deltaTime * 4;
			this.transform.position = Vector3.Lerp(startingPosition, finalPosition, t);
		}else {
			SwitchState<FloorButtonIdle>();
		}
	}
	
}
