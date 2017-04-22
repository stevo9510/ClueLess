using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StandardValueRepository : Singleton<StandardValueRepository>
{
    #region Game Icons

    public Sprite CardSpriteStudy;
    public Sprite CardSpriteHall;
    public Sprite CardSpriteLounge;
    public Sprite CardSpriteLibrary;
    public Sprite CardSpriteBilliardRoom;
    public Sprite CardSpriteDiningRoom;
    public Sprite CardSpriteConservatory;
    public Sprite CardSpriteBallroom;
    public Sprite CardSpriteKitchen;
    public Sprite CardSpriteScarlet;
    public Sprite CardSpriteMustard;
    public Sprite CardSpriteOrchid;
    public Sprite CardSpriteGreen;
    public Sprite CardSpritePeacock;
    public Sprite CardSpritePlum;
    public Sprite CardSpriteCandlestick;
    public Sprite CardSpriteDagger;
    public Sprite CardSpriteLeadPipe;
    public Sprite CardSpriteRevolver;
    public Sprite CardSpriteRope;
    public Sprite CardSpriteWrench;

    #endregion

    public event EventHandler PlayerDetailsChanged;

    public StandardEnums.PlayerEnum clientPlayerID;

    private Dictionary<StandardEnums.PlayerEnum, string> defaultPlayerNames;
    private Dictionary<StandardEnums.PlayerEnum, string> playerNames;
    private Dictionary<StandardEnums.CardEnum, Sprite> cardSpriteMapping;
    private Dictionary<StandardEnums.LocationEnum, string> locationNames;
    private Dictionary<StandardEnums.WeaponEnum, string> weaponNames;

    public Dictionary<StandardEnums.RoomEnum, StandardEnums.CardEnum> RoomToCardEnumMapping;
    public Dictionary<StandardEnums.PlayerEnum, StandardEnums.CardEnum> PlayerToCardEnumMapping;
    public Dictionary<StandardEnums.WeaponEnum, StandardEnums.CardEnum> WeaponToCardEnumMapping;
    public Dictionary<StandardEnums.RoomEnum, StandardEnums.LocationEnum> RoomToLocationEnumMapping;

    protected override void Awake()
    {
        base.Awake();

        InitializeEntityToCardMappings();
        InitializeDefaultPlayerNames();
        InitializeLocationNames();
        InitializeWeaponNames();
        InitializeCardSpriteMapping();
        SetPlayerNamesToDefault();
    }

    private void InitializeEntityToCardMappings()
    {
        this.RoomToCardEnumMapping = new Dictionary<StandardEnums.RoomEnum, StandardEnums.CardEnum>();
        this.PlayerToCardEnumMapping = new Dictionary<StandardEnums.PlayerEnum, StandardEnums.CardEnum>();
        this.WeaponToCardEnumMapping = new Dictionary<StandardEnums.WeaponEnum, StandardEnums.CardEnum>();
        this.RoomToLocationEnumMapping = new Dictionary<StandardEnums.RoomEnum, StandardEnums.LocationEnum>();

        RoomToCardEnumMapping[StandardEnums.RoomEnum.Ballroom] = StandardEnums.CardEnum.Ballroom;
        RoomToCardEnumMapping[StandardEnums.RoomEnum.BilliardRoom] = StandardEnums.CardEnum.BilliardRoom;
        RoomToCardEnumMapping[StandardEnums.RoomEnum.Conservatory] = StandardEnums.CardEnum.Conservatory;
        RoomToCardEnumMapping[StandardEnums.RoomEnum.DiningRoom] = StandardEnums.CardEnum.DiningRoom;
        RoomToCardEnumMapping[StandardEnums.RoomEnum.Hall] = StandardEnums.CardEnum.Hall;
        RoomToCardEnumMapping[StandardEnums.RoomEnum.Kitchen] = StandardEnums.CardEnum.Kitchen;
        RoomToCardEnumMapping[StandardEnums.RoomEnum.Library] = StandardEnums.CardEnum.Library;
        RoomToCardEnumMapping[StandardEnums.RoomEnum.Lounge] = StandardEnums.CardEnum.Lounge;
        RoomToCardEnumMapping[StandardEnums.RoomEnum.Study] = StandardEnums.CardEnum.Study;

        PlayerToCardEnumMapping[StandardEnums.PlayerEnum.Green] = StandardEnums.CardEnum.Green;
        PlayerToCardEnumMapping[StandardEnums.PlayerEnum.Mustard] = StandardEnums.CardEnum.Mustard;
        PlayerToCardEnumMapping[StandardEnums.PlayerEnum.Orchid] = StandardEnums.CardEnum.Orchid;
        PlayerToCardEnumMapping[StandardEnums.PlayerEnum.Peacock] = StandardEnums.CardEnum.Peacock;
        PlayerToCardEnumMapping[StandardEnums.PlayerEnum.Plum] = StandardEnums.CardEnum.Plum;
        PlayerToCardEnumMapping[StandardEnums.PlayerEnum.Scarlet] = StandardEnums.CardEnum.Scarlet;

        WeaponToCardEnumMapping[StandardEnums.WeaponEnum.Candlestick] = StandardEnums.CardEnum.Candlestick;
        WeaponToCardEnumMapping[StandardEnums.WeaponEnum.Dagger] = StandardEnums.CardEnum.Dagger;
        WeaponToCardEnumMapping[StandardEnums.WeaponEnum.LeadPipe] = StandardEnums.CardEnum.LeadPipe;
        WeaponToCardEnumMapping[StandardEnums.WeaponEnum.Revolver] = StandardEnums.CardEnum.Revolver;
        WeaponToCardEnumMapping[StandardEnums.WeaponEnum.Rope] = StandardEnums.CardEnum.Rope;
        WeaponToCardEnumMapping[StandardEnums.WeaponEnum.Wrench] = StandardEnums.CardEnum.Wrench;

        RoomToLocationEnumMapping[StandardEnums.RoomEnum.Ballroom] = StandardEnums.LocationEnum.Ballroom;
        RoomToLocationEnumMapping[StandardEnums.RoomEnum.BilliardRoom] = StandardEnums.LocationEnum.BilliardRoom;
        RoomToLocationEnumMapping[StandardEnums.RoomEnum.Conservatory] = StandardEnums.LocationEnum.Conservatory;
        RoomToLocationEnumMapping[StandardEnums.RoomEnum.DiningRoom] = StandardEnums.LocationEnum.DiningRoom;
        RoomToLocationEnumMapping[StandardEnums.RoomEnum.Hall] = StandardEnums.LocationEnum.Hall;
        RoomToLocationEnumMapping[StandardEnums.RoomEnum.Kitchen] = StandardEnums.LocationEnum.Kitchen;
        RoomToLocationEnumMapping[StandardEnums.RoomEnum.Library] = StandardEnums.LocationEnum.Library;
        RoomToLocationEnumMapping[StandardEnums.RoomEnum.Lounge] = StandardEnums.LocationEnum.Lounge;
        RoomToLocationEnumMapping[StandardEnums.RoomEnum.Study] = StandardEnums.LocationEnum.Study;
    }

    private void InitializeCardSpriteMapping()
    {
        cardSpriteMapping = new Dictionary<StandardEnums.CardEnum, Sprite>();
        cardSpriteMapping[StandardEnums.CardEnum.Study] = CardSpriteStudy;
        cardSpriteMapping[StandardEnums.CardEnum.Hall] = CardSpriteHall;
        cardSpriteMapping[StandardEnums.CardEnum.Lounge] = CardSpriteLounge;
        cardSpriteMapping[StandardEnums.CardEnum.Library] = CardSpriteLibrary;
        cardSpriteMapping[StandardEnums.CardEnum.BilliardRoom] = CardSpriteBilliardRoom;
        cardSpriteMapping[StandardEnums.CardEnum.DiningRoom] = CardSpriteDiningRoom;
        cardSpriteMapping[StandardEnums.CardEnum.Conservatory] = CardSpriteConservatory;
        cardSpriteMapping[StandardEnums.CardEnum.Ballroom] = CardSpriteBallroom;
        cardSpriteMapping[StandardEnums.CardEnum.Kitchen] = CardSpriteKitchen;
        cardSpriteMapping[StandardEnums.CardEnum.Scarlet] = CardSpriteScarlet;
        cardSpriteMapping[StandardEnums.CardEnum.Mustard] = CardSpriteMustard;
        cardSpriteMapping[StandardEnums.CardEnum.Orchid] = CardSpriteOrchid;
        cardSpriteMapping[StandardEnums.CardEnum.Green] = CardSpriteGreen;
        cardSpriteMapping[StandardEnums.CardEnum.Peacock] = CardSpritePeacock;
        cardSpriteMapping[StandardEnums.CardEnum.Plum] = CardSpritePlum;
        cardSpriteMapping[StandardEnums.CardEnum.Candlestick] = CardSpriteCandlestick;
        cardSpriteMapping[StandardEnums.CardEnum.Dagger] = CardSpriteDagger;
        cardSpriteMapping[StandardEnums.CardEnum.LeadPipe] = CardSpriteLeadPipe;
        cardSpriteMapping[StandardEnums.CardEnum.Revolver] = CardSpriteRevolver;
        cardSpriteMapping[StandardEnums.CardEnum.Rope] = CardSpriteRope;
        cardSpriteMapping[StandardEnums.CardEnum.Wrench] = CardSpriteWrench; 
    }

    public Sprite GetCardSprite(StandardEnums.CardEnum cardID)
    {
        return cardSpriteMapping[cardID];
    }

    public Sprite GetWeaponCardSprite(StandardEnums.WeaponEnum weaponID)
    {
        return cardSpriteMapping[WeaponToCardEnumMapping[weaponID]];
    }

    public Sprite GetRoomCardSprite(StandardEnums.RoomEnum roomID)
    {
        return cardSpriteMapping[RoomToCardEnumMapping[roomID]];
    }

    public Sprite GetPlayerCardSprite(StandardEnums.PlayerEnum playerID)
    {
        return cardSpriteMapping[PlayerToCardEnumMapping[playerID]];
    }

    private void InitializeLocationNames()
    {
        locationNames = new Dictionary<StandardEnums.LocationEnum, string>();
        locationNames[StandardEnums.LocationEnum.Study] = "Study";
        locationNames[StandardEnums.LocationEnum.Hall] = "Hall";
        locationNames[StandardEnums.LocationEnum.Lounge] = "Lounge";
        locationNames[StandardEnums.LocationEnum.Library] = "Library";
        locationNames[StandardEnums.LocationEnum.BilliardRoom] = "Billiard Room";
        locationNames[StandardEnums.LocationEnum.DiningRoom] = "Dining Room";
        locationNames[StandardEnums.LocationEnum.Conservatory] = "Conservatory";
        locationNames[StandardEnums.LocationEnum.Ballroom] = "Ballroom";
        locationNames[StandardEnums.LocationEnum.Kitchen] = "Kitchen";
    }

    private void InitializeWeaponNames()
    {
        weaponNames = new Dictionary<StandardEnums.WeaponEnum, string>();
        weaponNames[StandardEnums.WeaponEnum.Candlestick] = "Candlestick";
        weaponNames[StandardEnums.WeaponEnum.Dagger] = "Dagger";
        weaponNames[StandardEnums.WeaponEnum.LeadPipe] = "Lead Pipe";
        weaponNames[StandardEnums.WeaponEnum.Revolver] = "Revolver";
        weaponNames[StandardEnums.WeaponEnum.Rope] = "Rope";
        weaponNames[StandardEnums.WeaponEnum.Wrench] = "Wrench";

    }

    private void InitializeDefaultPlayerNames()
    {
        defaultPlayerNames = new Dictionary<StandardEnums.PlayerEnum, string>();
        // set default names of players
        defaultPlayerNames[StandardEnums.PlayerEnum.Scarlet] = "Miss Scarlet";
        defaultPlayerNames[StandardEnums.PlayerEnum.Mustard] = "Col. Mustard";
        defaultPlayerNames[StandardEnums.PlayerEnum.Orchid] = "Dr. Orchid";
        defaultPlayerNames[StandardEnums.PlayerEnum.Green] = "Mr. Green";
        defaultPlayerNames[StandardEnums.PlayerEnum.Peacock] = "Mrs. Peacock";
        defaultPlayerNames[StandardEnums.PlayerEnum.Plum] = "Professor Plum";
    }

    private void SetPlayerNamesToDefault()
    {
        playerNames = new Dictionary<StandardEnums.PlayerEnum, string>();
        // initialize player names to the default names by default
        foreach (var kvp in defaultPlayerNames)
        {
            playerNames[kvp.Key] = kvp.Value;
        }
    }

    public string GetPlayerName(StandardEnums.PlayerEnum playerID)
    {
        return playerNames[playerID];
    }

    public void UpdatePlayerName(StandardEnums.PlayerEnum playerID, string name)
    {
        playerNames[playerID] = name;
    }

    public string GetLocationName(StandardEnums.LocationEnum locationID)
    {
        return locationNames[locationID];
    }

    public string GetRoomName(StandardEnums.RoomEnum roomID)
    {
        var locationID = RoomToLocationEnumMapping[roomID];
        return locationNames[locationID];
    }

    public string GetWeaponName(StandardEnums.WeaponEnum weaponID)
    {
        return weaponNames[weaponID];
    }

    public string GetCardName(StandardEnums.CardEnum cardID)
    {
        if (PlayerToCardEnumMapping.ContainsValue(cardID))
            return GetPlayerName(PlayerToCardEnumMapping.Where(v => v.Value == cardID).First().Key);
        else if (WeaponToCardEnumMapping.ContainsValue(cardID))
            return GetWeaponName(WeaponToCardEnumMapping.Where(v => v.Value == cardID).First().Key);
        else if (RoomToCardEnumMapping.ContainsValue(cardID))
            return GetRoomName(RoomToCardEnumMapping.Where(v => v.Value == cardID).First().Key);
        return string.Empty;
    }

    public void GetHallwayLocations(StandardEnums.LocationEnum locationID, out string locNameOne, out string locNameTwo)
    {
        switch (locationID)
        {
            case StandardEnums.LocationEnum.BallKitch:
                locNameOne = locationNames[StandardEnums.LocationEnum.Ballroom];
                locNameTwo = locationNames[StandardEnums.LocationEnum.Kitchen];
                break;
            case StandardEnums.LocationEnum.BillBall:
                locNameOne = locationNames[StandardEnums.LocationEnum.BilliardRoom];
                locNameTwo = locationNames[StandardEnums.LocationEnum.Ballroom];
                break;
            case StandardEnums.LocationEnum.BillDin:
                locNameOne = locationNames[StandardEnums.LocationEnum.BilliardRoom];
                locNameTwo = locationNames[StandardEnums.LocationEnum.DiningRoom];
                break;
            case StandardEnums.LocationEnum.ConBall:
                locNameOne = locationNames[StandardEnums.LocationEnum.Conservatory];
                locNameTwo = locationNames[StandardEnums.LocationEnum.Ballroom];
                break;
            case StandardEnums.LocationEnum.DinKitch:
                locNameOne = locationNames[StandardEnums.LocationEnum.DiningRoom];
                locNameTwo = locationNames[StandardEnums.LocationEnum.Kitchen];
                break;
            case StandardEnums.LocationEnum.HallBill:
                locNameOne = locationNames[StandardEnums.LocationEnum.Hall];
                locNameTwo = locationNames[StandardEnums.LocationEnum.BilliardRoom];
                break;
            case StandardEnums.LocationEnum.HallLounge:
                locNameOne = locationNames[StandardEnums.LocationEnum.Hall];
                locNameTwo = locationNames[StandardEnums.LocationEnum.Lounge];
                break;
            case StandardEnums.LocationEnum.LibBill:
                locNameOne = locationNames[StandardEnums.LocationEnum.Library];
                locNameTwo = locationNames[StandardEnums.LocationEnum.BilliardRoom];
                break;
            case StandardEnums.LocationEnum.LibCon:
                locNameOne = locationNames[StandardEnums.LocationEnum.Library];
                locNameTwo = locationNames[StandardEnums.LocationEnum.Conservatory];
                break;
            case StandardEnums.LocationEnum.LoungeDin:
                locNameOne = locationNames[StandardEnums.LocationEnum.Lounge];
                locNameTwo = locationNames[StandardEnums.LocationEnum.DiningRoom];
                break;
            case StandardEnums.LocationEnum.StudyHall:
                locNameOne = locationNames[StandardEnums.LocationEnum.Study];
                locNameTwo = locationNames[StandardEnums.LocationEnum.Hall];
                break;
            case StandardEnums.LocationEnum.StudyLib:
                locNameOne = locationNames[StandardEnums.LocationEnum.Study];
                locNameTwo = locationNames[StandardEnums.LocationEnum.Library];
                break;
            default:
                throw new ArgumentException("An invalid hallway location was passed. ", "locationID");
        }

    }

    private void FirePlayerDetailChanged()
    {
        var handler = PlayerDetailsChanged;
        if (handler != null)
            handler(this, EventArgs.Empty);
    }

}
