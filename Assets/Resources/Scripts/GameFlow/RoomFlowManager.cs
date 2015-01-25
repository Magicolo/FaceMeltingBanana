using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Magicolo;

public class RoomFlowManager : MonoBehaviour {

	public static RoomFlowManager instance;
	private GameObject player;
	
	public Room currentRoom;
	public Room firstRoom;
	public List<Room> rooms;
	
	public Image fadeImage;
	private Color fromColor;
	private Color toColor;
	private float t = 0;
	private bool fadeOut = false;
	private bool fading = false;
	
	public Dictionary<string, Room> RoomsBanane;
	
	private bool roomLose = false;
	private bool switching = false;
	
	
	void Awake(){
		RoomFlowManager.instance = this;
		player = GameObject.FindWithTag("Player");
	}
	
	
	void Start () {
		switchToRoom(firstRoom);
	}
	
	
	void Update () {
		if(!switching && player.transform.position.y <= -10f){
			switchToRoom(currentRoom.looseRoom);
		}
		if(fading){
			t+= Time.deltaTime;
			fadeImage.color = Color.Lerp(fromColor,toColor, t);
			if(t >= 1){
				if(fadeOut){
					fading = false;
					fadeImage.color = toColor;
				}else{
					fadeOut = true;
					toColor = new Color(0,0,0,0);
					fromColor = new Color(0,0,0,1);
					t = 0;
				}
			}
		}
	}
	
	public void switchToRoom(Room room){
		//Debug.Log("switchToRoom");
		if(switching) return;
		//Debug.Log("llala");
		
		if(fadeImage != null){
			fromColor = new Color(0,0,0,0);
			toColor = new Color(0,0,0,1);
			fadeOut = false;
			fading = true;
			t = 0;
		}
		switching = true;
		player.transform.position = room.startingPosition;
		roomLose = false;
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
		player.transform.rotation = Quaternion.AngleAxis(this.currentRoom.startingAngle, Vector3.up);
		player.SetActive(true);
		switching = false;
	}
	
	public void goToNextRoom(){
		if(this.currentRoom.nextRoom == null){
			Application.LoadLevel("TheEnd");
		}else{
			switchToRoom(this.currentRoom.nextRoom);
		}
		
	}
	
	public void activeState(string stateName){
	
	}

	public void die(){
		if(!roomLose){
			letTheGroundFall();
			roomLose = true;
		}
	}

	private void letTheGroundFall(){
		foreach (BoxCollider box in this.currentRoom.GetComponentsInChildren<BoxCollider>()) {
			if(box.gameObject.transform.position.y <= 0){
				Rigidbody rb = box.gameObject.AddComponent<Rigidbody>();
				rb.mass = Random.Range(1f, 5f);
				rb.drag = Random.Range(0f, 5f); 
			}
		}
	}
}
