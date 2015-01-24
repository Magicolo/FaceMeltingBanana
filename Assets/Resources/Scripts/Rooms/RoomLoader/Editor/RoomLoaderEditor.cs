using UnityEditor;
using UnityEngine;
using System.Collections;


[System.Serializable]
public class RoomLoaderEditor : EditorWindow {

	
	public string fileName = "";
	public GameObject roomGameObject;
	public RoomLoaderLinker linker;
	
	void OnGUI(){
		roomGameObject = (GameObject) EditorGUILayout.ObjectField("Room Root", roomGameObject, typeof(GameObject), true);
		linker = (RoomLoaderLinker) EditorGUILayout.ObjectField("Room Loader Linker", linker, typeof(RoomLoaderLinker), true);
		//showFogOFWar = EditorGUILayout.Toggle("Show Fog of War", showFogOFWar);
		
		addFileLine();
		if (GUILayout.Button ("Load Map")) {
			RoomLoader loader = new RoomLoader(roomGameObject);
			loader.roomLoaderLinker = linker;
			loader.loadFromFile(fileName);
		}
	}

	void addFileLine(){
		GUILayout.BeginHorizontal ();
		fileName = GUILayout.TextField (fileName);
		if (GUILayout.Button ("Open Map File")) {
			fileName = EditorUtility.OpenFilePanel("Open Map File","maps","tmx");
		}
		GUILayout.EndHorizontal ();
	}
	
	[MenuItem ("FruitsUtils/Map Loader")]
	public static void ShowWindow(){
		EditorWindow.GetWindow(typeof(RoomLoaderEditor), true, "Room Loader");
	}
}
