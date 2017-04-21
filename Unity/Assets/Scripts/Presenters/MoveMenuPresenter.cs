using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to Handle all the Move related Presenter logic. 
/// For example:
///   - Listens for what move options a Player has and dynamically displays them in the <see cref="MoveOptionHost">
///   - Handles the MoveSelected by the Player and sends it to the Server
///   - If a move requiring a Suggestion is made, the user is prompted to select in the following order.
///      1. A Character to Suggest
///      2. A Weapon to Suggest
///   - If an accusation is made, the user is prompted to select in the following order:
///      1. A Room to Accuse
///      2. A Character to Accuse
///      3. A Weapon to Accuse
///   - These suggestion/accusation options are handled by transitions from the main game board to other views that allow the entities
///     to be selected
/// </summary>
public class MoveMenuPresenter : MonoBehaviour {

    [Tooltip("Place Prefab used to instantiate each move option in the list")]
    public GameObject MoveOptionPrefab;

    [Tooltip("Place the top level host of all the child move options here")]
    public GameObject MoveOptionHost;

    [Tooltip("Place the GameObject that is the Character Card Picker Host (that contains a CardSelectorPresenter)")]
    public GameObject CharacterCardHost;

    [Tooltip("Place the GameObject that is the Weapon Card Picker Host (that contains a CardSelectorPresenter)")]
    public GameObject WeaponCardHost;

    [Tooltip("Place the GameObject that is the Room Card Picker Host (that contains a CardSelectorPresenter)")]
    public GameObject RoomCardHost;

    /* Respective presenters from the CardHost objects */
    private CardSelectorPresenter CharacterCardSelectorPresenter;
    private CardSelectorPresenter WeaponCardSelectorPresenter;
    private CardSelectorPresenter RoomCardSelectorPresenter;

    /// <summary>
    /// The default camera position is cached to be able to move back to it once the card selectors are shown
    /// </summary>
    private Vector3 defaultCameraPosition;

    /// <summary>
    /// The current cached move message that will be sent to the server once the move is completed. 
    /// Move is sent once all required fields are filled out, for example, if suggestion needs to be made, all suggested entities need to be selected first
    /// </summary>
    private MoveMessage cachedMoveMessage;

	// Use this for initialization
	void Start ()
    {
        // Get the Presenter from the card host components
        this.CharacterCardSelectorPresenter = CharacterCardHost.GetComponent<CardSelectorPresenter>();
        this.WeaponCardSelectorPresenter = WeaponCardHost.GetComponent<CardSelectorPresenter>();
        this.RoomCardSelectorPresenter = RoomCardHost.GetComponent<CardSelectorPresenter>();

        // cache the default camera position
        this.defaultCameraPosition = Camera.main.transform.position;

        // Clear out move options on start.
        ClearAllMoveOptions();

        // TODO: Subscribe to server listener for when move options are available.

        // TODO: Test code.  Comment me out later
        var mockObject1 = new MoveViewModel(StandardEnums.MoveEnum.MoveToHallway, StandardEnums.LocationEnum.DinKitch);
        var mockObject2 = new MoveViewModel(StandardEnums.MoveEnum.MoveToRoomAndSuggest, StandardEnums.LocationEnum.Library);
        var mockObject3 = new MoveViewModel(StandardEnums.MoveEnum.StayInRoomAndSuggest, StandardEnums.LocationEnum.Lounge);
        var mockObject4 = new MoveViewModel(StandardEnums.MoveEnum.TakeSecretPassageAndSuggest, StandardEnums.LocationEnum.Hall);
        var mockObject5 = new MoveViewModel(StandardEnums.MoveEnum.MakeAnAccusation, null);
        var mockObject6 = new MoveViewModel(StandardEnums.MoveEnum.EndTurn, null);
        var mockObject7 = new MoveViewModel(StandardEnums.MoveEnum.MoveToHallway, StandardEnums.LocationEnum.LibBill);

        AddMoveOptions(new List<MoveViewModel>() { mockObject1, mockObject2, mockObject3, mockObject4, mockObject5, mockObject6, mockObject7});

        cachedMoveMessage = null;
    }

    #region Add Move Options

    /// <summary>
    /// Takes in a list of move options and instantiates move option controls for each move
    /// Wires up to move option button to handle move being made by player.
    /// </summary>
    /// <param name="moveViewModels"></param>
    private void AddMoveOptions(List<MoveViewModel> moveViewModels)
    {
        foreach (MoveViewModel moveVM in moveViewModels)
        {
            // dynamically create a move item
            GameObject moveOptionItem = Instantiate(MoveOptionPrefab);
            // set its parent in Unity to the Host
            moveOptionItem.transform.SetParent(this.MoveOptionHost.transform, false);
            // get the view to set the Text, and add a listener to the button
            MoveOptionView moveViewModel = moveOptionItem.GetComponent<MoveOptionView>();
            moveViewModel.textBox.text = moveVM.ToString(); // this ToString() provides a user friendly name for the ViewModel
            // Wire up button click event
            moveViewModel.button.onClick.AddListener(() => MoveButtonClickHandler(moveVM));
        }
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

    #endregion

    /// <summary>
    /// When move button is clicked, make the move for the player.
    /// </summary>
    /// <param name="movePerformed"></param>
    private void MoveButtonClickHandler(MoveViewModel movePerformed)
    {
        // Start creating the MoveMessage object and see if suggestion/accusation info is needed
        cachedMoveMessage = new MoveMessage(movePerformed.MoveID, movePerformed.LocationID);
        
        switch (movePerformed.MoveID)
        {
            // If the MoveID requires a suggestion to happen, start prompting for Character Selection
            case StandardEnums.MoveEnum.MoveToRoomAndSuggest:
            case StandardEnums.MoveEnum.TakeSecretPassageAndSuggest:
            case StandardEnums.MoveEnum.StayInRoomAndSuggest:
                PromptForCharacterSelection();
                break;
            
            // If the MoveID is for an accusation, start prompting for a Room Selection
            case StandardEnums.MoveEnum.MakeAnAccusation:
                PromptForRoomSelection();
                break;

            // Otherwise, we have all the info we need and we can send the result to the server!
            case StandardEnums.MoveEnum.MoveToHallway:
            case StandardEnums.MoveEnum.EndTurn:
                SendMoveToServer();
                break;
        }
        
    }

    /// <summary>
    /// Step 1: (For Accusations) Choose a Room
    /// N/A: (For Suggestions) Because Room is the Location the player moved to
    /// Move to the RoomCardHost picker, and subscribe to the card selected event
    /// </summary>
    private void PromptForRoomSelection()
    {
        MoveCameraToGameObjectWithSpecifiedDistance(RoomCardHost, -25.0F);
        RoomCardSelectorPresenter.CardSelected += RoomCardSelectorPresenter_CardSelected;
    }

    /// <summary>
    /// When a Room Card is Selected, unsubscribe from that event and set that locationID to the CachedMoveMessage.
    /// Then request a CharacterSelection (Step 2 for Accusations)
    /// </summary>
    /// <param name="roomID"></param>
    private void RoomCardSelectorPresenter_CardSelected(int roomID)
    {
        RoomCardSelectorPresenter.CardSelected -= RoomCardSelectorPresenter_CardSelected;
        cachedMoveMessage.LocationID = (StandardEnums.LocationEnum)roomID;
        PromptForCharacterSelection();
    }

    /// <summary>
    /// Step 2: (For Accusations) Choose a Character (Step 1: for Suggestions)
    /// Move to the RoomCardHost picker, and subscribe to the card selected event
    /// </summary>
    private void PromptForCharacterSelection()
    {
        MoveCameraToGameObjectWithSpecifiedDistance(CharacterCardHost, -17.0F);
        CharacterCardSelectorPresenter.CardSelected += CharacterCardSelectorPresenter_CardSelected;
    }

    /// <summary>
    /// When a Character Card is Selected, unsubscribe from that event and set that playerID to the CachedMoveMessage.
    /// Then request a WeaponSelection (Step 3 for Accusations; Step 2 for Suggestions)
    /// </summary>
    /// <param name="selectedPlayerID"></param>
    private void CharacterCardSelectorPresenter_CardSelected(int selectedPlayerID)
    {
        CharacterCardSelectorPresenter.CardSelected -= CharacterCardSelectorPresenter_CardSelected;
        cachedMoveMessage.PlayerID = (StandardEnums.PlayerEnum)selectedPlayerID;
        PromptForWeaponSelection();
    }

    /// <summary>
    /// Step 3: (For Accusations) Choose a Weapon (Step 2: For Suggestions)
    /// Move to the WeaponCardHost picker, and subscribe to the card selected event
    /// </summary>
    private void PromptForWeaponSelection()
    {
        MoveCameraToGameObjectWithSpecifiedDistance(WeaponCardHost, -17.0F);
        WeaponCardSelectorPresenter.CardSelected += WeaponCardSelectorPresenter_CardSelected;
    }

    /// <summary>
    /// When a Weapon Card is Selected, unsubscribe from that event and set that weaponID to the CachedMoveMessage.
    /// Now the message is complete and ready to send to the Server
    /// </summary>
    /// <param name="selectedPlayerID"></param>
    private void WeaponCardSelectorPresenter_CardSelected(int selectedWeaponID)
    {
        WeaponCardSelectorPresenter.CardSelected -= WeaponCardSelectorPresenter_CardSelected;
        cachedMoveMessage.WeaponID = (StandardEnums.WeaponEnum)selectedWeaponID;
        Camera.main.transform.position = this.defaultCameraPosition;
        SendMoveToServer();
    }

    /// <summary>
    /// Send the cached move message to the server
    /// </summary>
    private void SendMoveToServer()
    {
        // Send move to server

        this.cachedMoveMessage = null;
        ClearAllMoveOptions();
    }

    /// <summary>
    /// Helper method to move the camera to the object, a fixed distance away.  
    /// If time permits a more mathematical approach could be used to determine the full view frustrum needed for the game object
    /// </summary>
    /// <param name="objectToMoveCameraTo"></param>
    /// <param name="distanceAway"></param>
    private void MoveCameraToGameObjectWithSpecifiedDistance(GameObject objectToMoveCameraTo, float distanceAway)
    {
        var transform = objectToMoveCameraTo.transform;
        Camera.main.transform.position = transform.position + (transform.forward * distanceAway);
    }

}

