using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {
	
	public enum PlayerTypes {
		Adventurer,
		Cartographer
	}
	
	public PlayerTypes playerType;
	
	// PlayerPrefs.SetString("MyString", "MyValue");
	int serverPort = 25003;
	int NoOfPlayersServer = 16;
	
	void Start() {
		StartServer();
	}
	
	void StartServer() {
		
		Network.InitializeServer(NoOfPlayersServer, serverPort, true);
		MasterServer.RegisterHost("ServerWorld", "World Server", "COMMENTAIRE OH ALLO :D ");
		Debug.Log("Server STARTED");
		//Network.TestConnection(
	}
	
	[RPC]
	void Send() {
		if (Network.connections.Length > 0) {
			networkView.RPC("Receive", Network.connections[0]);
		}
	}
	
	[RPC]
	void Receive(object position) {
		Logger.Log(position);
	}
}
