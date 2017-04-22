using SocketIO;
using System;
using UnityEngine;

public class Network : Singleton<Network>, IServerToClientMessagePublisher
{

    static SocketIOComponent socket;

    public event Action<AccusationMoveMadeMessage> EventAccusationMoveMade;
    public event Action<AccusationResultMessage> EventAccusationResultReceived;
    public event Action<CardsDealtMessage> EventCardsDealt;
    public event Action<MoveOptionMessage> EventMoveOptionsReceived;
    public event Action<PlayerAssignedIDMessage> EventPlayerAssignedID;
    public event Action<PlayerMovedMessage> EventPlayerMoved;
    public event Action<PlayersInGameMessage> EventPlayersInGameChanged;
    public event Action<PlayerTurnMessage> EventPlayerTurnChanged;
    public event Action<SuggestionDebunkMessage> EventSuggestionDebunkReceived;
    public event Action<SuggestionMoveMadeMessage> EventSuggestionMoveMade;
    public event Action<SuggestionProveOptionMessage> EventSuggestionProveOptionsReceived;
    public event Action<SuggestionProveTurnMessage> EventSuggestionProveTurnChanged;
    public event Action<WeaponMovedMessage> EventWeaponMoved;

    // Use this for initialization
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("AccusationMoveMade", OnAccusationMoveMade);
        socket.On("AccusationResult", OnAccusationResultReceived);
        socket.On("CardsDealt", OnCardsDealt);
        socket.On("MoveOptions", OnMoveOptionsReceived);
        socket.On("PlayerAssignedID", OnPlayerAssignedID);
        socket.On("PlayerMoved", OnPlayerMoved);
        socket.On("PlayersInGameChanged", OnPlayersInGameChanged);
        socket.On("PlayerTurnChanged", OnPlayerTurnChanged);
        socket.On("SuggestionDebunk", OnSuggestionDebunkReceived);
        socket.On("SuggestionMoveMade", OnSuggestionMoveMade);
        socket.On("SuggestionProveOptions", OnSuggestionProveOptionsReceived);
        socket.On("SuggestionProveTurnChanged", OnSuggestionProveTurnChanged);
        socket.On("WeaponMoved", OnWeaponMoved);
    }

    #region Listeners / Handlers -- Everything in here deserializes the data and fires an event

    private void OnAccusationResultReceived(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventAccusationMoveMade);
    }

    private void OnAccusationMoveMade(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventAccusationMoveMade);
    }

    private void OnCardsDealt(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventCardsDealt);
    }

    private void OnMoveOptionsReceived(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventMoveOptionsReceived);
    }

    private void OnPlayerAssignedID(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventPlayerAssignedID);
    }

    private void OnPlayerMoved(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventPlayerMoved);
    }

    private void OnPlayersInGameChanged(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventPlayersInGameChanged);
    }

    private void OnPlayerTurnChanged(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventPlayerTurnChanged);
    }

    private void OnSuggestionDebunkReceived(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventSuggestionDebunkReceived);
    }

    private void OnSuggestionMoveMade(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventSuggestionMoveMade);
    }

    private void OnSuggestionProveOptionsReceived(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventSuggestionProveOptionsReceived);
    }

    private void OnSuggestionProveTurnChanged(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventSuggestionProveTurnChanged);
    }

    private void OnWeaponMoved(SocketIOEvent msg)
    {
        DeserializeAndFireEvent(msg, EventWeaponMoved);
    }

    #endregion

    #region Publishers

    public void ConnectToGame()
    {
        socket.Connect();
    }

    public void SendMoveMessage(MoveMessage msg)
    {
        string data = JsonUtility.ToJson(msg);
        socket.Emit("makeMove", new JSONObject(data));
    }

    public void SendSuggestionProveAnswer(SuggestionProveMessage msg)
    {
        string data = JsonUtility.ToJson(msg);
        socket.Emit("disproveSuggestion", new JSONObject(data));
    }

    #endregion

    private void DeserializeAndFireEvent<T>(SocketIOEvent msg, Action<T> actionEvent)
    {
        // deserialize object
        var deserializedObj = JsonUtility.FromJson<T>(msg.data.ToString());
        // fire event to notify subscribers
        SafelyFireEvent(actionEvent, deserializedObj);
    }

    /// <summary>
    /// Safely invoke an action with a generic parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="act"></param>
    /// <param name="args"></param>
    private void SafelyFireEvent<T>(Action<T> act, T args)
    {
        var handler = act;
        if (handler != null)
            handler(args);
    }

    void OnConnected(SocketIOEvent obj)
    {
        Debug.Log("connected");
        //socket.Emit ("say hi");
    }

}
