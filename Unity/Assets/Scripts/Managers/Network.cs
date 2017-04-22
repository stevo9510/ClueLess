using SocketIO;
using UnityEngine;

public class Network : Singleton<Network> {

	static SocketIOComponent socket;

	// Use this for initialization
	void Start () {
		socket = GetComponent<SocketIOComponent> ();
        socket.On("open", OnConnected);
    }

    public void ConnectToGame()
    {
        socket.Connect();
    }

    void OnConnected(SocketIOEvent obj){
        Debug.Log("connected");
        //socket.Emit ("say hi");
    }
	
}
