using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	 void OnTriggerEnter(Collider other) {
		RoomFlowManager.instance.goToNextRoom();
    }
}
