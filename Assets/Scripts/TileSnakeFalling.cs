using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnakeFalling : State {

	public override void OnEnter() {
		rigidbody.isKinematic = false;
	}
}

