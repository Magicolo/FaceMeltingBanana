using UnityEngine;
using System.Collections;
using Magicolo;

public class Door : StateLayer {

	[Disable] public GameObject cube;
	[Disable] public PuzzleBase puzzle;
	[Disable] public SimplePuzzle simplePuzzle;
	[Disable] public MeshRenderer cubeMeshRenderer;
	
	public Color baseColor;
	
	public bool enterWin = true;
	
	
	public override void OnAwake() {
		base.OnAwake();
		
		cube = gameObject.FindChild("Cube");
		cubeMeshRenderer = cube.GetComponent<MeshRenderer>();
		cubeMeshRenderer.material.color = baseColor;
	}
	
	public override void TriggerEnter(Collider collision) {
		base.TriggerEnter(collision);

		if (enterWin && collision.gameObject.tag == "Player") {
			RoomFlowManager.instance.goToNextRoom();
		}
	}
	
	public void Open() {
		SwitchState<DoorOpening>();
	}
	
	public void Close() {
		SwitchState<DoorClose>();
	}
}
