using UnityEngine;
using System.Collections;
using Magicolo;

public class Door : StateLayer {

	[Disable] public GameObject cube;
	[Disable] public PuzzleBase puzzle;
	[Disable] public MeshRenderer cubeMeshRenderer;
	
	public Color baseColor;
	
	public bool enterWin = true;
	
	
	public override void OnAwake() {
		base.OnAwake();
		
		cube = gameObject.FindChild("Cube");
		cubeMeshRenderer = cube.GetComponent<MeshRenderer>();
	}
	
	public override void TriggerEnter(Collider collision) {
		base.TriggerEnter(collision);

		if (enterWin && collision.gameObject.name == "Player") {
			RoomFlowManager.instance.goToNextRoom();
		}
	}
	
	public void Open() {
		Logger.Log("Open");
		SwitchState<DoorOpening>();
	}
	
	public void Close() {
		SwitchState<DoorClose>();
	}
}
