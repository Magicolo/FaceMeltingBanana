using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			RoomFlowManager.instance.die();
		}
    }
}
