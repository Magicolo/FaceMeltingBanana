using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class PuzzleBase : StateLayer {

	[Disable] public string successString;
	[Disable] public string failString;
	[Disable] public List<Door> doors;
	
	public override void OnStart() {
		base.OnStart();

		doors = new List<Door>();
		
		foreach (Door door in transform.parent.GetComponentsInChildren<Door>()) {
			doors.Add(door);
			door.puzzle = this;
		}
	}
	
	public virtual void Success() {
	}
	
	public virtual void Fail() {
	}
}

