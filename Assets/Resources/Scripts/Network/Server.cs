using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {
	//client related
	public int clientPort = 25003;
	public string ip = "10.212.8.139";
	bool connected = false;


	//server related
	bool serverOnline = false;



	//both
	public enum PlayerTypes {
		Adventurer,
		Cartographer
	}
	
	public PlayerTypes playerType;
	
	// PlayerPrefs.SetString("MyString", "MyValue");
	int serverPort = 25003;
	int NoOfPlayersServer = 16;

	void Start() {
		//Sera setter par le Main Menu
		int playerType = PlayerPrefs.GetInt("PlayerType");

		if (playerType==0){
			PlayerTypes = PlayerTypes.Adventurer;
			StartServer();

		} else {
			PlayerTypes = PlayerTypes.Cartographer;
			StartClient();
		}
		
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
	
	[RPC]
	void Receive(object position){
		Logger.Log(position);
	}






























}
