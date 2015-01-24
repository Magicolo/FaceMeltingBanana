using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class KeySnakePickedUp : State {

	KeySnake Layer {
		get { return ((KeySnake)layer); }
	}
	
	public override void OnEnter() {
		if (Layer.debug) {
			Logger.Log(string.Format("You have picked the {0} key.", Layer.isNextKey ? "correct" : "wrong"));
		}
		
		if (Layer.isNextKey) {
			if (Layer.nextKey == null) {
				Layer.puzzle.playerHasLastKey = true;
				Layer.puzzle.OpenDoors();
			}
			else {
				Layer.nextKey.isNextKey = true;
			}
		}
		else {
			Layer.puzzle.Fail();
		}
		
		gameObject.Remove();
	}
}
