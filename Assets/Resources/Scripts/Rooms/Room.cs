using UnityEngine;
using System.Collections;
using Magicolo;

public class Room : MonoBehaviour {

	public Room looseRoom;
	public Room nextRoom;
	public Vector3 startingPosition;
	public float startingAngle;
	
	public int width;
	public int height;
	
	public string fileName;
	[ButtonAttribute("Open Map","openMap", NoPrefixLabel = true)] public bool map;
	
	void Start () {
	
	}
	
	void openMap(){
		#if UNITY_EDITOR
		fileName = UnityEditor.EditorUtility.OpenFilePanel("Open Map File","maps","tmx");
		#endif
	}
	
	
	void Update () {
	
	}
}
