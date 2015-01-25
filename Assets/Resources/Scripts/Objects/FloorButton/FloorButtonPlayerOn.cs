using UnityEngine;
using System.Collections;
using Magicolo;

public class FloorButtonPlayerOn : State {

	FloorButton Layer {
		get { return ((FloorButton)layer); }
	}
	
	private float t;
	private Vector3 startingPosition;
	private Vector3 finalPosition;
	private bool exited;
    
	public override void OnEnter() {
		exited = false;
		t = 0;
		Layer.simplePuzzle.handleMessage("btn",this.Layer.keyword);
		startingPosition = this.transform.position;
		finalPosition = new Vector3(startingPosition.x, startingPosition.y - 0.09f, startingPosition.z);
	}
	
	public override void OnUpdate() {
		if(t<1){
			t+= Time.deltaTime * 4;
			this.transform.position = Vector3.Lerp(startingPosition, finalPosition, t);
		}else if(exited){
			SwitchState<FloorButtonPlayerOff>();
		}
	}
	
	public override void TriggerExit(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			exited = true;
		}
	}
	
	
}
