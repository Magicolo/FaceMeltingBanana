using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Magicolo;

public class RoomFlowManager : MonoBehaviour {

	public static RoomFlowManager instance;
	public GameObject player;
	
	public Room currentRoom;
	public Room firstRoom;
	public List<Room> rooms;
	
	
	void Awake(){
		RoomFlowManager.instance = this;
	}
	
	
	void Start () {
		foreach (var element in rooms) {
			element.gameObject.SetActive(false);
		}
		switchToRoom(firstRoom);
	}
	
	
	void Update () {
	
	}
	
	public void switchToRoom(Room room){
		if(this.currentRoom != null){
			foreach (var element in this.currentRoom.gameObject.GetChildren()) {
				Destroy(element);
			}
			this.currentRoom.gameObject.SetActive(false);
		}
		GameObjectExtend.createClone(room.roomPrefab,"RoomThings", room.transform, Vector3.zero);
		this.currentRoom = room;
		player.transform.position = room.startingPosition;
		
	}
	
	public void goToNextRoom(){
		switchToRoom(this.currentRoom.nextRoom);
	}
	
	public void activeState(string stateName){
	
	}
}
