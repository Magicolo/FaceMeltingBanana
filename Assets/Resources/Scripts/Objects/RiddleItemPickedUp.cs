using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class RiddleItemPickedUp : State {

	RiddleItem Layer {
		get { return ((RiddleItem)layer); }
	}
	
	public override void OnEnter() {
		if (Layer.debug) {
			Logger.Log(string.Format("You have chosen the {0} item.", Layer.isCorrectChoice ? "correct" : "wrong"));
		}
		
		gameObject.Remove();
	}
}

