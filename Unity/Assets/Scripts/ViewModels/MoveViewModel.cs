/// <summary>
/// Model that is used for binding to a View and helping build a user friendly name for the move
/// </summary>
public class MoveViewModel
{
    public StandardEnums.MoveEnum MoveID { get; private set; }

    public StandardEnums.LocationEnum LocationID { get; private set; }

    public MoveViewModel(StandardEnums.MoveEnum pMoveID)
    {
        this.MoveID = pMoveID;
    }

    public MoveViewModel(StandardEnums.MoveEnum pMoveID, StandardEnums.LocationEnum pLocID)
    {
        this.MoveID = pMoveID;
        this.LocationID = pLocID;
    }

    /// <summary>
    /// Set a friendly display name for this move
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        switch(MoveID)
        {
            case StandardEnums.MoveEnum.EndTurn:
                return "End Turn";
            case StandardEnums.MoveEnum.MakeAnAccusation:
                return "Make an Accusation...";
            case StandardEnums.MoveEnum.MoveToHallway:
                string room1;
                string room2;
                StandardValueRepository.Instance.GetHallwayLocations(LocationID, out room1, out room2);
                return string.Format("Move to Hallway Between {0} and {1}", room1, room2);
            case StandardEnums.MoveEnum.MoveToRoomAndSuggest:
                return string.Format("Move to {0} and Make Suggestion...", GetRoomName());
            case StandardEnums.MoveEnum.StayInRoomAndSuggest:
                return string.Format("Stay in {0} and Make Suggestion...", GetRoomName());
            case StandardEnums.MoveEnum.TakeSecretPassageAndSuggest:
                return string.Format("Take Secret Passage to {0} and Make Suggestion...", GetRoomName());
        }
        return "Invalid Move ID";
    }

    /// <summary>
    /// Shorthand method for getting the name of the room
    /// </summary>
    /// <returns></returns>
    private string GetRoomName()
    {
        return StandardValueRepository.Instance.GetRoomName(LocationID);
    }
}
