using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class PuzzleBase : StateLayer {

	[Disable] public string successString;
	[Disable] public string failString;
	[Disable] public List<Door> doors;
	
	public override void OnAwake() {
		base.OnAwake();
		
		doors = new List<Door>();
		
		foreach (Transform child in transform.parent.GetChildrenRecursive()) {
			if (child.name == "Door") {
				doors.Add(child.GetComponent<Door>());
			}
		}
	}
}

