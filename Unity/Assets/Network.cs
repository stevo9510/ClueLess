using SocketIO;
using UnityEngine;

public class Network : MonoBehaviour {

	static SocketIOComponent socket;


	// Use this for initialization
	void Start () {
		socket = GetComponent<SocketIOComponent> ();
		socket.On ("open", OnConnected);
	}


	void OnConnected(SocketIOEvent obj){
		Debug.Log ("connected");
		socket.Emit ("say hi");
	}




	
	// Update is called once per frame
	void Update () {
		
	}
}
