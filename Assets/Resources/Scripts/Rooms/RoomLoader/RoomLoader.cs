﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using Magicolo;

public class RoomLoader : TiledMapLoader {

	
	private GameObject parentGameObject;
	private Room room;
	
	private Transform currentLayer;
	private int currentUnityLayer;
	private SimplePuzzle simplePuzzle = null;
	
	public RoomLoaderLinker roomLoaderLinker;
	
	
	public RoomLoader(GameObject parentGameObject){
		this.parentGameObject = parentGameObject;
		clearParent();
	}

	void clearParent(){
		foreach (var element in this.parentGameObject.GetChildren()) {
			UnityEngine.Object.DestroyImmediate(element);
		}
		room = parentGameObject.GetOrAddComponent<Room>();
	}
	
	
	protected override void loadMapProperty(string name, string value){
		if(name == "PlayerStartingPosition"){
			string[] positions = value.Split(new char[]{','});
			float x = float.Parse(positions[0]);
			float z = this.room.height - float.Parse(positions[1]);
			room.startingPosition = new Vector3(x,1,z);
		}else if(name == "PlayerStartingAngle"){
			this.room.startingAngle = float.Parse(value);
		}else if(name == "-PuzzleId"){
			int index = Int32.Parse(value);
			GameObject prefab = this.roomLoaderLinker.PuzzleBasePrefabs[index];
			GameObject newGo = GameObjectExtend.createClone(prefab, prefab.name, room.transform, Vector3.zero);
			simplePuzzle = newGo.GetComponent<SimplePuzzle>();
		}else if(name == "=FloorButtonSequence"){
			putButtonSequence(value);
		}
	}

	void putButtonSequence(string value){
		String[] keys = value.Split(new char[]{','});
		KeySquenceLink firstLink = new KeySquenceLink(keys[0]);
		KeySquenceLink currentLink = firstLink;
		
		for (int i = 1; i < keys.Length; i++) {
			KeySquenceLink newLink = new KeySquenceLink(keys[i]); 
			currentLink.next = newLink;
			currentLink = newLink;
		}
		
		FloorButtonPuzzle puzzle = UnityEngine.Object.FindObjectOfType<FloorButtonPuzzle>();
		puzzle.firstKeySquenceLink = firstLink;
		puzzle.currentKeySquenceLink = firstLink;
	}
	
	protected override void afterMapAttributesLoaded(){
		this.room.width = this.mapWidth;
		this.room.height = this.mapHeight;
	}

	protected override void afterMapPropertiesLoaded(){
		
	}

	protected override void addLayer(string layerName, int width, int height, Dictionary<string, string> properties){
		this.currentLayer = GameObjectExtend.createGameObject(layerName, parentGameObject.transform, Vector3.zero, false).transform;
		currentUnityLayer = -1;
		if(properties.ContainsKey("UnityLayer")){
			currentUnityLayer = LayerMask.NameToLayer(properties["UnityLayer"]);
		}
		
		if(properties.ContainsKey("Offset")){
			int offset = Int32.Parse(properties["Offset"]);
			currentLayer.transform.Translate(new Vector3(0,offset,0));
		}
	}
	
	
	protected override void addTile(int x, int y, int id){
		GameObject newPrefab = getNewPrefab(id);
		
		if(newPrefab != null){
			Vector3 newP = new Vector3(x,0,y);
			GameObject newGo = GameObjectExtend.createClone(newPrefab, newPrefab.name, currentLayer, newP, true);
			if(currentUnityLayer != -1){
				newGo.gameObject.layer = currentUnityLayer;
				var fils = newGo.GetChildren();
				foreach (var element in fils) {
					element.gameObject.layer = currentUnityLayer;
				}
			}
			if(this.tilesetTiles.ContainsKey(id)){
				Dictionary<String,String> properties = this.tilesetTiles[id];
				addPropertiesTo(newGo, properties);
			}
		}
	}

	void addPropertiesTo(GameObject newGo, Dictionary<string, string> properties){
		if(properties.ContainsKey("SnakeKey")){
			KeySnake ks = newGo.GetComponent<KeySnake>();
			ks.index = Int32.Parse(properties["SnakeKey"]);
		}
		if(properties.ContainsKey("FloorButton")){
			FloorButton fb = newGo.GetComponent<FloorButton>();
			fb.keyword = properties["FloorButton"];
		}
		if(properties.ContainsKey("DoorLocked")){
			Door door = newGo.GetComponent<Door>();
			if(properties["DoorLocked"] == "false"){
				door.SwitchState<DoorOpen>();
			}
		}
		if(properties.ContainsKey("DoorColor")){
			Door door = newGo.GetComponent<Door>();
			String[] colorData = properties["DoorColor"].Split(new char[]{','});
			Int32 r = Int32.Parse(colorData[0]);
			Int32 g = Int32.Parse(colorData[1]);
			Int32 b = Int32.Parse(colorData[2]);
			door.baseColor = new Color(r/255f,g/255f,b/255f,1);
			door.FindChild("Cube").GetComponent<MeshRenderer>().material.color = door.baseColor;
		}
		if(properties.ContainsKey("DoorWin")){
			Door door = newGo.GetComponent<Door>();
			door.enterWin = properties["DoorWin"] == "true";
		}
		if(properties.ContainsKey("DoorKeyword")){
			Door door = newGo.GetComponent<Door>();
			door.keyword = properties["DoorKeyword"];
		}
		if(properties.ContainsKey("Collider")){
			BoxCollider box = newGo.GetComponentInChildren<BoxCollider>();
			if(properties["Collider"] == "false"){
				box.enabled = false;
			}
			
		}
	}
	
	
	GameObject getNewPrefab(int id){
		if(roomLoaderLinker.blockPrefabs.Count < id){
			Debug.LogError("Une tuile d'id " + id + " n'a pas de prefab disponible dans le linker.");
		}else{
			GameObject prefab = roomLoaderLinker.blockPrefabs[id];
			if(prefab == null){
				Debug.LogWarning("Une tuile d'id " + id + " est null.");
			}else{
				return prefab;
			}
		}
		return null;
	}
	protected override void addObject(string objectGroupName, int x, int y, Dictionary<string, string> properties){
		
	}
	

	protected override void afterAll(){
		if(this.simplePuzzle != null){
			simplePuzzle.init();
		}
	}
}
