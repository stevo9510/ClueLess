using System;

/// <summary>
/// Interface for each event that fires for each message received from the server. 
/// This provides a centralized publisher for all of these events.
/// </summary>
public interface IServerToClientMessagePublisher
{
    event Action<AccusationMoveMadeMessage> EventAccusationMoveMade;
    event Action<AccusationResultMessage> EventAccusationResultReceived;
    event Action<CardsDealtMessage> EventCardsDealt;
    event Action<MoveOptionMessage> EventMoveOptionsReceived;
    event Action<PlayerAssignedIDMessage> EventPlayerAssignedID;
    event Action<PlayerMovedMessage> EventPlayerMoved;
    event Action<PlayersInGameMessage> EventPlayersInGameChanged;
    event Action<PlayerTurnMessage> EventPlayerTurnChanged;
    event Action<SuggestionDebunkMessage> EventSuggestionDebunkReceived;
    event Action<SuggestionMoveMadeMessage> EventSuggestionMoveMade;
    event Action<SuggestionProveOptionMessage> EventSuggestionProveOptionsReceived;
    event Action<SuggestionProveTurnMessage> EventSuggestionProveTurnChanged;
    event Action<WeaponMovedMessage> EventWeaponMoved;
}
