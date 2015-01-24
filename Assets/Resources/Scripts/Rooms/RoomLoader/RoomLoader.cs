using System;
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
		}else if(name == "PuzzleId"){
			int index = Int32.Parse(value);
			GameObject prefab = this.roomLoaderLinker.PuzzleBasePrefabs[index];
			GameObjectExtend.createClone(prefab, prefab.name, room.transform, Vector3.zero);
		}
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
			GameObject newGo = GameObjectExtend.createClone(newPrefab, newPrefab.name, currentLayer, newP,true);
			if(currentUnityLayer != -1){
				newGo.gameObject.layer = currentUnityLayer;
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
	
}
