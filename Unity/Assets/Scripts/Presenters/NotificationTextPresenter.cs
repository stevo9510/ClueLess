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
        notificationTextbox.text = string.Empty;
        for(int ind=1; ind <= 6; ind++)
        {
            MessagePublisher_EventAccusationResultReceived(new AccusationResultMessage()
            { playerID = (StandardEnums.PlayerEnum)ind, roomID = (StandardEnums.RoomEnum)ind, weaponID = (StandardEnums.WeaponEnum)ind});
        }
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
            accusedPlayer, accusedWeapon, accusedRoom));

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
        string locationName = GetLocationName(obj.locationID);
        AddNotification(string.Format("{0} has moved to {1}", playerName, locationName));
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
        this.notificationTextbox.text = "*" + text + Environment.NewLine + this.notificationTextbox.text;
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

    private string GetLocationName(StandardEnums.LocationEnum locationID)
    {
        return StandardValueRepository.Instance.GetLocationName(locationID);
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
