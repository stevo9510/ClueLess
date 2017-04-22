using System;

/// <summary>
/// Interface for each event that fires for each message received from the server. 
/// This provides a centralized publisher for all of these events.
/// </summary>
public interface IServerToClientMessagePublisher
{
    event Action<AccusationMoveMadeMessage> AccusationMoveMade;
    event Action<AccusationResultMessage> AccusationResultReceived;
    event Action<CardsDealtMessage> CardsDealt;
    event Action<PlayerAssignedIDMessage> PlayerAssignedID;
    event Action<PlayerMovedMessage> PlayerMoved;
    event Action<PlayersInGameMessage> PlayersInGameChanged;
    event Action<PlayerTurnMessage> PlayerTurnChanged;
    event Action<SuggestionDebunkMessage> SuggestionDebunkReceived;
    event Action<SuggestionMoveMadeMessage> SuggestionMoveMade;
    event Action<SuggestionProveOptionMessage> SuggestionProveOptionsReceived;
    event Action<SuggestionProveTurnMessage> SuggestionProveTurnChanged;
    event Action<WeaponMovedMessage> WeaponMoved;
}
