using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Player : MonoBehaviourExtended {

	public enum PlayerTypes {
		Adventurer,
		Cartographer
	}
	
	public PlayerTypes playerType;
	
	void Update() {
		if (playerType == PlayerTypes.Adventurer) {
			
		}
	}
	
	[RPC]
	void Send() {
		networkView.RPC("Receive", RPCMode.Others, transform.position);
	}
	
	[RPC]
	void Receive(object position){
		Logger.Log(position);
	}
}