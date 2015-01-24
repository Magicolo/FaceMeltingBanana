using UnityEngine;
using System.Collections;
using Magicolo;

public class Door : StateLayer {

	public override void TriggerEnter(Collider collision) {
		base.TriggerEnter(collision);
		RoomFlowManager.instance.goToNextRoom();
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
