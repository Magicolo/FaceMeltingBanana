using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnake : StateLayer {

	public override void OnAwake() {
		base.OnAwake();
		GetComponentInChildren<MeshRenderer>().material.color = Color.red;
	}
}