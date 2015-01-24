using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {
	int clientPort = 25003;
	string ip = "127.0.0.1";
	bool connected = false;
	void ConnectToServer() {
		Network.Connect(ip,clientPort);
	}
	
	void OnConnectedToServer() {
		connected = true;
	}
	
	void OnDisconnectedFromServer() {
		connected = false;
	}
	
	// Use this for initialization
	void Start () {
		ConnectToServer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseOver(){
		if(Input.GetMouseButton(0)){
			Debug.Log("Clicked : Connect to server");
			// Whatever you want it to do.
			Debug.Log("Retrieved informations : " );
			Debug.Log("Connected : " + connected);
		}
	}
}