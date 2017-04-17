using System;
using System.Collections.Generic;

public class StandardValueRepository : Singleton<StandardValueRepository>
{
    public event EventHandler PlayerDetailsChanged;

    private Dictionary<StandardEnums.PlayerEnum, string> defaultPlayerNames;
    private Dictionary<StandardEnums.PlayerEnum, string> playerNames;

    private Dictionary<StandardEnums.LocationEnum, string> locationNames; 

    protected override void Awake()
    {
        base.Awake();
        InitializeDefaultPlayerNames();
        InitializeLocationNames();
        SetPlayerNamesToDefault();
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

    public void GetHallwayLocations(StandardEnums.LocationEnum locationID, out string locNameOne, out string locNameTwo)
    {
        switch(locationID)
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
