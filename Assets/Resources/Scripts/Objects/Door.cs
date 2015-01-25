using UnityEngine;
using System.Collections;
using Magicolo;

public class Door : StateLayer {

	public float speed = 8;
	[Disable] public GameObject cube;
	[Disable] public PuzzleBase puzzle;
	
	public override void OnStart() {
		base.OnStart();
		
		cube = gameObject.FindChild("Cube");
	}
	
	public override void TriggerEnter(Collider collision) {
		base.TriggerEnter(collision);

		if (collision.gameObject.name == "Player") {
			puzzle.Success();
		}
	}
	
	public void Open() {
		Logger.Log("Open");
		SwitchState<DoorOpen>();
	}
	
	public void Close() {
		SwitchState<DoorClose>();
	}
}
