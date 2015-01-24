using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class PuzzleSnake : PuzzleBase {

	[Disable] public bool playerHasLastKey;
	[Disable] public GameObject player;
	
	public override void OnStart() {
		base.OnStart();
		
		player = GameObject.Find("Player");
		
		KeySnake[] keys = transform.parent.GetComponentsInChildren<KeySnake>();
		int[] indices = new int[keys.Length];
		
		for (int i = 0; i < keys.Length; i++) {
			KeySnake key = keys[i];
			
			indices[i] = key.index;
			key.puzzle = this;
		}
		
		System.Array.Sort(indices, keys);
		
		for (int i = 0; i < keys.Length - 1; i++) {
			KeySnake key = keys[i];
			
			if (i == 0){
				key.isNextKey = true;
			}
			
			key.nextKey = keys[i + 1];
		}
	}

	public void Fail() {
		if (GetActiveState() != GetState<PuzzleSnakeFail>()) {
			SwitchState<PuzzleSnakeFail>();
		}
	}
}