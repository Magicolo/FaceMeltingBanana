using UnityEngine;
using System.Collections;
using Magicolo;

public class Door : StateLayer {

	public float speed = 8;
	[Disable] public GameObject cube;
	
	public override void OnStart() {
		base.OnStart();
		
		cube = gameObject.FindChild("Cube");
	}
	
	public override void TriggerEnter(Collider collision) {
		base.TriggerEnter(collision);

		if (collision.gameObject.name == "Player") {
			RoomFlowManager.instance.goToNextRoom();
		}
	}
	
	public void Open() {
		if (GetActiveState() != GetState<DoorOpen>()) {
			SwitchState<DoorOpen>();
		}
	}
	
	public void Close() {
		if (GetActiveState() != GetState<DoorClose>()) {
			SwitchState<DoorClose>();
		}
	}
}
