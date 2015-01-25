using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {
	//client related
	public int clientPort = 25003;
	public string ip = "127.0.0.1";
	bool connected = false;
	
	string messageAuClient="nulll";
	//server related
	bool serverOnline = false;
	int serverPort = 25003;
	int NoOfPlayersServer = 16;
	
	
	//both
	public enum PlayerTypes {
		Adventurer,
		Cartographer
	}
	
	public PlayerTypes playerTypes;
	
	public int pInt=0;

	void Start() {
		//Sera setter par le Main Menu
		//int pInt = PlayerPrefs.GetInt("PlayerType");

		//Enable le FPS
		if (pInt==0){
			GameObject.Find("CameraTopView").GetComponent<Camera>().enabled=false;
			GameObject.Find("Main Camera").GetComponent<Camera>().enabled=true;
			playerTypes = PlayerTypes.Adventurer;
			StartServer();
			
		} 
		//Enable le retard qui tient une map (Cartman)
		else {
			GameObject.Find("CameraTopView").GetComponent<Camera>().enabled=true;
			GameObject.Find("Main Camera").GetComponent<Camera>().enabled=false;
			playerTypes = PlayerTypes.Cartographer;
			ConnectToServer();
		}
		
	}
	
	void StartServer() {
		
		Network.InitializeServer(NoOfPlayersServer, serverPort, true);
		MasterServer.RegisterHost("ServerWorld", "World Server", "Serveur demarrer");
		Debug.Log("Server StartServer() ok");
	
	}

	[RPC]
	public void envoyerChangementLevel(string roomName){

		Debug.Log ("envoyerChgt:" + roomName + "nbr de conn : " + Network.connections.Length  );
		if (Network.connections.Length > 0 && Network.isServer) {
			Debug.Log ("ok jenvoie ca au client retard qui est connecter (cartman)");
			networkView.RPC ("recevoirChangementLevel", RPCMode.Others, roomName);
		}
	}

	[RPC]
	public 	void recevoirChangementLevel(string message){
		if(!Network.isServer){
			Debug.Log ("Message recu du server:" + message);
			RoomFlowManager.instance.switchToRoom(message);
			messageAuClient = "recevoirChangementLevel: "+ message;
		}
	}


	[RPC]
	public 	void Send() {
		if (Network.connections.Length > 0) {
			Debug.Log ("networkView send Receive");
			networkView.RPC("Receive", Network.connections[0]);
		
		}
	}
	
	[RPC]
	public void Receive(object position) {
		Logger.Log(position);
	}
	
	void ConnectToServer() {
		Network.Connect(ip,clientPort);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.U)){
			Debug.Log("Manual input Connect to server");
			ConnectToServer();
			Debug.Log("Retrieved informations : " );
			Debug.Log("Connected : " + connected);
		}
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Debug.Log("Manual input GOTOROOM A");
			envoyerChangementLevel("RoomA");

		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Debug.Log("Manual input GOTOROOM B");
			envoyerChangementLevel("RoomB");
			
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Debug.Log("Manual input GOTOROOM C");
			envoyerChangementLevel("RoomC");
			
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			Debug.Log("Manual input GOTOROOM D");
			envoyerChangementLevel("RoomD");
			
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			Debug.Log("Manual input GOTOROOM E");
			envoyerChangementLevel("RoomE");
			
		}
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			Debug.Log("Manual input GOTOROOM G");
			envoyerChangementLevel("RoomG");
			
		}
		if(Input.GetKeyDown(KeyCode.Alpha7)){
			Debug.Log("Manual input GOTOROOM L");
			envoyerChangementLevel("RoomL");
			
		}
	}

	void OnGUI(){
		string z = "isClient:" + Network.isClient.ToString() + " M:" + messageAuClient;
		if (GUI.Button (new Rect (10,10,800,100), z)){
			print ("You clicked the button!");
		}
			
	}
		
	void OnConnectedToServer() {
		connected = true;
	}
	
	void OnDisconnectedFromServer() {
		connected = false;
	}

}
