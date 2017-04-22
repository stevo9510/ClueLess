using System;

/// <summary>
/// Stub server to client message publisher implementation just to use temporarily until 
/// actual implementation in Socket.IO class is implemented.
/// </summary>
public class StubServerToClientMessagePublisher : IServerToClientMessagePublisher
{
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
}
