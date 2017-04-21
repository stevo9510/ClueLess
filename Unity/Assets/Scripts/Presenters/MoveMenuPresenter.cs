using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveMenuPresenter : MonoBehaviour {

    private Vector3 defaultCameraPosition;

    /// <summary>
    /// Prefab used to instantiate each Move Option in the list
    /// </summary>
    public GameObject MoveOptionPrefab;

    /// <summary>
    /// Host of Game Move Options
    /// </summary>
    public GameObject MoveOptionHost;

    public GameObject CharacterCardHost;

    public GameObject WeaponCardHost;

    public GameObject RoomCardHost;

    private CardSelectorPresenter CharacterCardSelectorPresenter;
    private CardSelectorPresenter WeaponCardSelectorPresenter;
    private CardSelectorPresenter RoomCardSelectorPresenter;

    private MoveMessage cachedMoveMessage;

	// Use this for initialization
	void Start () {

        this.CharacterCardSelectorPresenter = CharacterCardHost.GetComponent<CardSelectorPresenter>();
        this.WeaponCardSelectorPresenter = WeaponCardHost.GetComponent<CardSelectorPresenter>();
        this.RoomCardSelectorPresenter = RoomCardHost.GetComponent<CardSelectorPresenter>();

        this.defaultCameraPosition = Camera.main.transform.position;

        // Clear out move options on start.
        ClearAllMoveOptions();

        // TODO: Subscribe to server listener for when move options are available.

        // TODO: Test code.  Comment me out later
        var mockObject1 = new MoveModel() { MoveID = 1, LocationID = 11 };
        var mockObject2 = new MoveModel() { MoveID = 2, LocationID = 1 };
        var mockObject25 = new MoveModel() { MoveID = 2, LocationID = 5 };

        var mockObject3 = new MoveModel() { MoveID = 3, LocationID = 3 };
        var mockObject4 = new MoveModel() { MoveID = 4, LocationID = 7 };
        var mockObject5 = new MoveModel() { MoveID = 5 };
        var mockObject6 = new MoveModel() { MoveID = 6 };

        AddMoveOptions(new List<MoveModel>() { mockObject1, mockObject2, mockObject25, mockObject3, mockObject4, mockObject5, mockObject6 });

        cachedMoveMessage = null;
    }

    /// <summary>
    /// Clear the move options
    /// </summary>
    private void ClearAllMoveOptions()
    {
        foreach (Transform child in MoveOptionHost.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Takes in a list of move options and instantiates move option controls for each move
    /// Wires up to move option button to handle move being made by player.
    /// </summary>
    /// <param name="moves"></param>
    private void AddMoveOptions(List<MoveModel> moves)
    {
        foreach (MoveModel move in moves)
        {
            GameObject moveOptionItem = Instantiate(MoveOptionPrefab);
            moveOptionItem.transform.SetParent(this.MoveOptionHost.transform, false);
            MoveOptionView moveViewModel = moveOptionItem.GetComponent<MoveOptionView>();
            moveViewModel.textBox.text = move.ToString();
            moveViewModel.button.onClick.AddListener(() => MoveButtonHandler(move));
        }
    }

    /// <summary>
    /// When move button is clicked, make the move for the player.
    /// </summary>
    /// <param name="movePerformed"></param>
    private void MoveButtonHandler(MoveModel movePerformed)
    {
        cachedMoveMessage = new MoveMessage()
        {
            MoveID = (StandardEnums.MoveEnum)movePerformed.MoveID,
        };
        
        if(movePerformed.LocationID.HasValue)
        {
            cachedMoveMessage.LocationID = (StandardEnums.LocationEnum)movePerformed.LocationID;
        }

        // TODO: Handle making game move.  Call to server to make game move
        switch ((StandardEnums.MoveEnum)movePerformed.MoveID)
        {
            case StandardEnums.MoveEnum.MoveToRoomAndSuggest:
            case StandardEnums.MoveEnum.TakeSecretPassageAndSuggest:
            case StandardEnums.MoveEnum.StayInRoomAndSuggest:
                PromptForCharacterSelection();
                break;

            case StandardEnums.MoveEnum.MoveToHallway:
            case StandardEnums.MoveEnum.EndTurn:
                SendMoveToServer();
                break;

            case StandardEnums.MoveEnum.MakeAnAccusation:
                PromptForRoomSelection();
                break;
        }
        
    }

    private void PromptForRoomSelection()
    {
        Camera.main.transform.position = RoomCardHost.transform.position + (RoomCardHost.transform.forward * -25.0F);
        RoomCardSelectorPresenter.CardSelected += RoomCardSelectorPresenter_CardSelected;
    }

    private void RoomCardSelectorPresenter_CardSelected(int roomID)
    {
        RoomCardSelectorPresenter.CardSelected -= RoomCardSelectorPresenter_CardSelected;
        cachedMoveMessage.LocationID = (StandardEnums.LocationEnum)roomID;
        PromptForCharacterSelection();
    }

    private void PromptForCharacterSelection()
    {
        Camera.main.transform.position = CharacterCardHost.transform.position + (CharacterCardHost.transform.forward * -17.0F);
        CharacterCardSelectorPresenter.CardSelected += CharacterCardSelectorPresenter_CardSelected;
    }

    private void CharacterCardSelectorPresenter_CardSelected(int selectedPlayerID)
    {
        CharacterCardSelectorPresenter.CardSelected -= CharacterCardSelectorPresenter_CardSelected;
        cachedMoveMessage.PlayerID = (StandardEnums.PlayerEnum)selectedPlayerID;
        PromptForWeaponSelection();
    }

    private void PromptForWeaponSelection()
    {
        Camera.main.transform.position = WeaponCardHost.transform.position + (WeaponCardHost.transform.forward * -17.0F);
        WeaponCardSelectorPresenter.CardSelected += WeaponCardSelectorPresenter_CardSelected;
    }

    private void WeaponCardSelectorPresenter_CardSelected(int selectedWeaponID)
    {
        WeaponCardSelectorPresenter.CardSelected -= WeaponCardSelectorPresenter_CardSelected;
        cachedMoveMessage.WeaponID = (StandardEnums.WeaponEnum)selectedWeaponID;
        Camera.main.transform.position = this.defaultCameraPosition;
        SendMoveToServer();
    }

    private void SendMoveToServer()
    {

        // Send move to server


        this.cachedMoveMessage = null;
        ClearAllMoveOptions();
    }
}

