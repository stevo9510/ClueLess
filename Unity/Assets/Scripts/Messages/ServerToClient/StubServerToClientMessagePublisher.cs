using System;

/// <summary>
/// Stub server to client message publisher implementation just to use temporarily until 
/// actual implementation in Socket.IO class is implemented.
/// </summary>
public class StubServerToClientMessagePublisher : IServerToClientMessagePublisher
{
    public event Action<AccusationMoveMadeMessage> AccusationMoveMade;
    public event Action<AccusationResultMessage> AccusationResultReceived;
    public event Action<CardsDealtMessage> CardsDealt;
    public event Action<PlayerAssignedIDMessage> PlayerAssignedID;
    public event Action<PlayerMovedMessage> PlayerMoved;
    public event Action<PlayersInGameMessage> PlayersInGameChanged;
    public event Action<PlayerTurnMessage> PlayerTurnChanged;
    public event Action<SuggestionDebunkMessage> SuggestionDebunkReceived;
    public event Action<SuggestionMoveMadeMessage> SuggestionMoveMade;
    public event Action<SuggestionProveOptionMessage> SuggestionProveOptionsReceived;
    public event Action<SuggestionProveTurnMessage> SuggestionProveTurnChanged;
    public event Action<WeaponMovedMessage> WeaponMoved;
}
