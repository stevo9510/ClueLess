using System;
using UnityEngine;
using UnityEngine.UI;

public class NotificationTextPresenter : MonoBehaviour {

    public Text notificationTextbox;
    public Scrollbar scrollBar;
    private IServerToClientMessagePublisher messagePublisher;

	// Use this for initialization
	void Start () {
        messagePublisher = Network.Instance;
        notificationTextbox.text = string.Empty;
        messagePublisher.EventAccusationMoveMade += MessagePublisher_EventAccusationMoveMade;
        messagePublisher.EventAccusationResultReceived += MessagePublisher_EventAccusationResultReceived;
        messagePublisher.EventCardsDealt += MessagePublisher_EventCardsDealt;
        messagePublisher.EventSuggestionProveTurnChanged += MessagePublisher_EventSuggestionProveTurnChanged;
        messagePublisher.EventSuggestionProveOptionsReceived += MessagePublisher_EventSuggestionProveOptionsReceived;
        messagePublisher.EventSuggestionMoveMade += MessagePublisher_EventSuggestionMoveMade;
        messagePublisher.EventSuggestionDebunkReceived += MessagePublisher_EventSuggestionDebunkReceived;
        messagePublisher.EventPlayerTurnChanged += MessagePublisher_EventPlayerTurnChanged;
        messagePublisher.EventPlayersInGameChanged += MessagePublisher_EventPlayersInGameChanged;
        messagePublisher.EventPlayerMoved += MessagePublisher_EventPlayerMoved;
        messagePublisher.EventPlayerAssignedID += MessagePublisher_EventPlayerAssignedID;
    }

    private void MessagePublisher_EventPlayerAssignedID(PlayerAssignedIDMessage obj)
    {
        StandardValueRepository.Instance.clientPlayerID = obj.playerID;
        AddNotification(string.Format("Successfully joined game!  You have been assigned to {0}", GetPlayerName(obj.playerID)));
    }

    private void MessagePublisher_EventAccusationMoveMade(AccusationMoveMadeMessage obj)
    {
        string playerNameThatMadeAccusation = GetPlayerName(obj.playerThatMadeAccusation);
        string accusedPlayer = GetPlayerName(obj.playerID);
        string accusedWeapon = GetWeaponName(obj.weaponID);
        string accusedRoom = GetRoomName(obj.roomID);
        AddNotification(string.Format("{0}: \"I accuse {1} of committing the crime in the {2} with the {3}.",
            playerNameThatMadeAccusation, accusedPlayer, accusedWeapon, accusedRoom));

        if(obj.isCorrect)
        {
            AddNotification(string.Format("{0} is correct has won the game!", playerNameThatMadeAccusation));
        }
        else
        {
            AddNotification(string.Format("{0} is incorrect and has been eliminated!", playerNameThatMadeAccusation));
        }
    }

    private void MessagePublisher_EventAccusationResultReceived(AccusationResultMessage obj)
    {
        string playerName = GetPlayerName(obj.playerID);
        string roomName = GetRoomName(obj.roomID);
        string weaponName = GetWeaponName(obj.weaponID);
        AddNotification(string.Format("The crime was commited by {0} in the {1} with the {2}", 
            playerName, roomName, weaponName));
    }

    private void MessagePublisher_EventCardsDealt(CardsDealtMessage obj)
    {
        AddNotification("Your cards have been dealt.  Click 'View Cards...' button to view.");
    }

    private void MessagePublisher_EventPlayerMoved(PlayerMovedMessage obj)
    {
        string playerName = GetPlayerName(obj.playerID);
        if(StandardValueRepository.Instance.RoomToLocationEnumMapping.ContainsValue(obj.locationID))
        {
            string roomName = GetRoomName(obj.locationID);
            AddNotification(string.Format("{0} has moved to {1}", playerName, roomName));
        }
        else
        {
            string room1;
            string room2;
            StandardValueRepository.Instance.GetHallwayLocations(obj.locationID, out room1, out room2);
            AddNotification(string.Format("{0} has moved to Hallway Between {1} and {2}", playerName, room1, room2));
        }
    }

    private void MessagePublisher_EventPlayersInGameChanged(PlayersInGameMessage obj)
    {
        // TODO: Not sure if we should do anything here yet
    }

    private void MessagePublisher_EventPlayerTurnChanged(PlayerTurnMessage obj)
    {
        string playerName = GetPlayerName(obj.playerID);
        AddNotification(string.Format("It is now {0}'s turn to move", playerName));
    }

    private void MessagePublisher_EventSuggestionDebunkReceived(SuggestionDebunkMessage obj)
    {
        string playerName = GetPlayerName(obj.playerID);
        string cardName = GetCardName(obj.cardID);
        AddNotification(string.Format("{0} has proven your suggestion wrong by showing you {1}", playerName, cardName));
    }

    private void MessagePublisher_EventSuggestionMoveMade(SuggestionMoveMadeMessage obj)
    {
        string playerMakingSuggestion = GetPlayerName(obj.playerMakingSuggestionID);
        string suggestedPlayer = GetPlayerName(obj.playerID);
        string suggestedRoom = GetRoomName(obj.roomID);
        string suggestedWeapon = GetWeaponName(obj.weaponID);
        AddNotification(string.Format("{0}: \"I suggest the crime was committed in the {1} by {2} with the {3}\"",
            playerMakingSuggestion, suggestedRoom, suggestedPlayer, suggestedWeapon));
    }

    private void MessagePublisher_EventSuggestionProveOptionsReceived(SuggestionProveOptionMessage obj)
    {
        AddNotification(string.Format("It is your turn to try and prove the suggestion wrong. Choose a card (if possible)"));
    }

    private void MessagePublisher_EventSuggestionProveTurnChanged(SuggestionProveTurnMessage obj)
    {
        if(!IsCurrentPlayer(obj.playerID))
        { 
            string playerName = GetPlayerName(obj.playerID);
            AddNotification(string.Format("It is now {0}'s turn to try and prove the suggestion wrong", playerName));
        }
    }

    #region Helper Methods

    private bool IsCurrentPlayer(StandardEnums.PlayerEnum playerID)
    {
        return playerID == StandardValueRepository.Instance.clientPlayerID;
    }

    private void AddNotification(string text)
    {
        string newLine = string.Empty;
        if (!string.IsNullOrEmpty(notificationTextbox.text.Trim()))
            newLine = Environment.NewLine;

        this.notificationTextbox.text = "•" + text + newLine + this.notificationTextbox.text;
        // scroll to bottom
    }

    private string GetWeaponName(StandardEnums.WeaponEnum weaponID)
    {
        return StandardValueRepository.Instance.GetWeaponName(weaponID);
    }

    private string GetRoomName(StandardEnums.RoomEnum roomID)
    {
        return StandardValueRepository.Instance.GetRoomName(roomID);
    }

    private string GetRoomName(StandardEnums.LocationEnum locationID)
    {
        return StandardValueRepository.Instance.GetRoomName(locationID);
    }

    private string GetPlayerName(StandardEnums.PlayerEnum playerID)
    {
        return StandardValueRepository.Instance.GetPlayerName(playerID);
    }

    private string GetCardName(StandardEnums.CardEnum cardID)
    {
        return StandardValueRepository.Instance.GetCardName(cardID);
    }

    #endregion
}
