/// <summary>
/// This class holds the standard enum values to reference 
/// for game objects.  These all can be cast into an INT to 
/// match against a corresponding message ID.
/// 
/// Please be careful with changing any of these values because some are hardcoded in the Unity designer (for simplicity in certain areas)
/// </summary>
public class StandardEnums
{
    /// <summary>
    /// IMPORTANT: IF you change the value of any the player enums, you will need to update each EntityID within the Unity Designer
    /// for the PlayerChooserPanel->PlayerPanelItemHost->Each Player Card's CardEnumTag component
    /// </summary>
    public enum PlayerEnum
    {
        Scarlet = 1,
        Mustard = 2,
        Orchid = 3,
        Green = 4,
        Peacock = 5,
        Plum = 6
    }


    /// <summary>
    /// IMPORTANT: IF you change the value of any the weapon enums, you will need to update each EntityID within the Unity Designer
    /// for the WeaponChooserPanel->WeaponPanelItemHost->Each Weapon Card's CardEnumTag component
    /// </summary>
    public enum WeaponEnum
    {
        Candlestick = 1,
        Dagger = 2,
        LeadPipe = 3,
        Revolver = 4,
        Rope = 5,
        Wrench = 6
    }

    /// <summary>
    /// First 9 are rooms; rest are hallways
    /// </summary>
    public enum LocationEnum
    {
        Study = 1,
        Hall = 2,
        Lounge = 3,
        Library = 4,
        BilliardRoom = 5,
        DiningRoom = 6,
        Conservatory = 7,
        Ballroom = 8,
        Kitchen = 9,
        StudyHall = 10,
        HallLounge = 11,
        StudyLib = 12, 
        HallBill = 13,
        LoungeDin = 14,
        LibBill = 15,
        BillDin = 16,
        LibCon = 17,
        BillBall = 18,
        DinKitch = 19,
        ConBall = 20,
        BallKitch = 21
    }

    /// <summary>
    /// IMPORTANT: IF you change the value of any the room enums, you will need to update each EntityID within the Unity Designer
    /// for the RoomChooserPanel->RoomPanelItemHost->Each Room Card's CardEnumTag component
    /// </summary>
    public enum RoomEnum
    {
        Study = 1,
        Hall = 2,
        Lounge = 3,
        Library = 4,
        BilliardRoom = 5,
        DiningRoom = 6,
        Conservatory = 7,
        Ballroom = 8,
        Kitchen = 9
    }

    public enum CardEnum
    {
        Study = 1,
        Hall = 2,
        Lounge = 3,
        Library = 4,
        BilliardRoom = 5,
        DiningRoom = 6,
        Conservatory = 7,
        Ballroom = 8,
        Kitchen = 9,
        Scarlet = 10,
        Mustard = 11,
        Orchid = 12,
        Green = 13,
        Peacock = 14,
        Plum = 15,
        Candlestick = 16,
        Dagger = 17,
        LeadPipe = 18,
        Revolver = 19,
        Rope = 20,
        Wrench = 21
    }

    public enum MoveEnum
    {
        MoveToHallway = 1,
        TakeSecretPassageAndSuggest = 2,
        MoveToRoomAndSuggest = 3, 
        StayInRoomAndSuggest = 4,
        MakeAnAccusation = 5,
        EndTurn = 6
    }

    public enum ClientToServerMessageEnum
    {
        ConnectToGame = 1,
        Move = 2,
        SuggestionProve = 3
    }

}
