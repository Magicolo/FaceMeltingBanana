using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {
	// PlayerPrefs.SetString("MyString", "MyValue");
	int serverPort = 25003;
	int NoOfPlayersServer = 16;
	
	// Use this for initialization
	void Start () {
		StartServer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void StartServer() {
		
		Network.InitializeServer(NoOfPlayersServer, serverPort, true);
		MasterServer.RegisterHost("ServerWorld", "World Server", "COMMENTAIRE OH ALLO :D ");
		Debug.Log("Server STARTED");
		//Network.TestConnection(
	}
	
	
	
}
