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
	}
	protected override void loadMapProperty(string name, string value){
		
	}

	protected override void afterMapAttributesLoaded(){
		
	}

	protected override void afterMapPropertiesLoaded(){
		
	}
	
	protected override void addTile(string layerName, int x, int y, int id){
		GameObject newPrefab = getNewPrefab(id);
		GameObject newGo;
		
		Vector3 newP = new Vector3(x,0,y);
		if(newPrefab != null){
			if(layerName == "Wall"){
				GameObjectExtend.createClone(newPrefab,"Wall", wallParent, newP);
			}else if(layerName == "Floor"){
				GameObjectExtend.createClone(newPrefab, "Floor", floorParent, newP);
			}else if(layerName == "CeilingP1"){
				newGo = GameObjectExtend.createClone(newPrefab, "CeilingP1", ceilingParent, newP);
				newGo.gameObject.layer = LayerMask.NameToLayer("P1 Only");
			}else if(layerName == "RoofP2"){
				newGo = GameObjectExtend.createClone(newPrefab, "RoofP2", roofParent, newP);
				newGo.gameObject.layer = LayerMask.NameToLayer("P2 Only");
			}else if(layerName == "Object"){
				GameObjectExtend.createClone(newPrefab, "Object", objectsParent, newP);
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
