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
	
	public Dictionary<string, Room> RoomsBanane;
	
	
	void Awake(){
		RoomFlowManager.instance = this;
	}
	
	
	void Start () {
		//foreach (var element in rooms) {
			//element.gameObject.SetActive(false);
		//}
		switchToRoom(firstRoom);
	}
	
	
	void Update () {
	
	}
	
	public void switchToRoom(Room room){
		player.SetActive(false);
		if(this.currentRoom != null){
			foreach (var element in this.currentRoom.gameObject.GetChildren()) {
				Destroy(element);
			}
		}
		RoomLoader loader = new RoomLoader(room.gameObject);
		loader.roomLoaderLinker = RoomLoaderLinker.instance;
		loader.loadFromFile(room.fileName);
		
		this.currentRoom = room;
		player.transform.position = room.startingPosition;
		player.SetActive(true);
	}
	
	public void goToNextRoom(){
		switchToRoom(this.currentRoom.nextRoom);
	}
	
	public void activeState(string stateName){
	
	}
}
