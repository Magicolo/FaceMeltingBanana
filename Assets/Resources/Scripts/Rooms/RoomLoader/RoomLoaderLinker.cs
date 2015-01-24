using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class RoomLoaderLinker : MonoBehaviour {

	public List<GameObject> blockPrefabs;
	public List<GameObject> PuzzleBasePrefabs;
	
	public static RoomLoaderLinker instance;
	
	void Awake(){
		RoomLoaderLinker.instance = this;
	}
	
}
