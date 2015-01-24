using System.Collections.Generic;
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
	
	private Transform wallParent;
	private Transform floorParent;
	private Transform ceilingParent;
	private Transform roofParent;
	private Transform objectsParent;
	
	public RoomLoaderLinker roomLoaderLinker;
	
	
	public RoomLoader(GameObject parentGameObject){
		this.parentGameObject = parentGameObject;
		clearParent();
		this.wallParent = GameObjectExtend.createGameObject("Wall", parentGameObject.transform, Vector3.zero, false).transform;
		this.floorParent = GameObjectExtend.createGameObject("Floor", parentGameObject.transform, Vector3.zero, false).transform;
		this.ceilingParent = GameObjectExtend.createGameObject("Ceiling P1", parentGameObject.transform, Vector3.zero, false).transform;
		ceilingParent.gameObject.layer = LayerMask.NameToLayer("P1 Only");
		this.roofParent = GameObjectExtend.createGameObject("Roof P2", parentGameObject.transform, Vector3.zero, false).transform;
		roofParent.gameObject.layer = LayerMask.NameToLayer("P2 Only");
		this.objectsParent = GameObjectExtend.createGameObject("object", parentGameObject.transform, Vector3.zero, false).transform;
	}

	void clearParent(){
		foreach (var element in this.parentGameObject.GetChildren()) {
			Object.DestroyImmediate(element);
		}
		room = parentGameObject.GetOrAddComponent<Room>();
	}
	
	
	protected override void loadMapProperty(string name, string value){
		if(name == "PlayerStartingPosition"){
			string[] positions = value.Split(new char[]{','});
			float x = float.Parse(positions[0]);
			float z = this.room.height - float.Parse(positions[1]);
			room.startingPosition = new Vector3(x,1,z);
		}
	}

	protected override void afterMapAttributesLoaded(){
		this.room.width = this.mapWidth;
		this.room.height = this.mapHeight;
	}

	protected override void afterMapPropertiesLoaded(){
		
	}
	
	protected override void addTile(string layerName, int x, int y, int id){
		GameObject newPrefab = getNewPrefab(id);
		
		if(newPrefab != null){
			Vector3 newP = new Vector3(x,0,y);
			Transform parent = null;
			int layer = -1;
			if(layerName == "Wall"){
				parent = wallParent;
			}else if(layerName == "Floor"){
				parent = floorParent;
			}else if(layerName == "CeilingP1"){
				parent = ceilingParent;
				layer = LayerMask.NameToLayer("P1 Only");
			}else if(layerName == "RoofP2"){
				parent = roofParent;
				layer = LayerMask.NameToLayer("P2 Only");
			}else if(layerName == "Object"){
				parent = objectsParent;
			}
			GameObject newGo = GameObjectExtend.createClone(newPrefab, newPrefab.name, parent, newP);
			if(layer != -1){
				newGo.gameObject.layer = layer;
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
	
}
