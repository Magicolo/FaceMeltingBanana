using UnityEngine;
using System.Collections;
using Magicolo;

public class PuzzleFloorButton : PuzzleBase {

	[Disable] public GameObject player;
	public KeySquenceLink firstKeySquenceLink;
	[Disable] public KeySquenceLink currentKeySquenceLink;
	
	public override void OnStart() {
		base.OnStart();
		
		player = GameObject.Find("Player");
		
		FloorButton[] floorButtons = transform.parent.GetComponentsInChildren<FloorButton>();
		foreach (var button in floorButtons) {
			button.puzzle = this;
		}
	}

	public void OpenDoors() {
		Logger.Log("OpenDoors");
		foreach (Door door in doors) {
			door.Open();
		}
	}
	
	public override void Fail() {
		if (GetActiveState() != GetState<PuzzleSnakeFail>()) {
			SwitchState<PuzzleSnakeFail>();
		}
	}
	
	public void registerKeyword(string key){
		this.GetState<PuzzleFloorButtonPlay>().registerKeyword(key);
	}
	
}
