public class MoveModel
{
    public int MoveID { get; set; }

    public int? LocationID { get; set; }

    public override string ToString()
    {
        switch((StandardEnums.MoveEnum)MoveID)
        {
            case StandardEnums.MoveEnum.EndTurn:
                return "End Turn";
            case StandardEnums.MoveEnum.MakeAnAccusation:
                return "Make an Accusation...";
            case StandardEnums.MoveEnum.MoveToHallway:
                string room1;
                string room2;
                StandardValueRepository.Instance.GetHallwayLocations((StandardEnums.LocationEnum)LocationID.Value,
                    out room1, out room2);
                return string.Format("Move to Hallway Between {0} and {1}", room1, room2);
            case StandardEnums.MoveEnum.MoveToRoomAndSuggest:
                return string.Format("Move to Room {0} and Make Suggestion...", GetRoomName());
            case StandardEnums.MoveEnum.StayInRoomAndSuggest:
                return string.Format("Stay in Room {0} and Make Suggestion...", GetRoomName());
            case StandardEnums.MoveEnum.TakeSecretPassageAndSuggest:
                return string.Format("Take Secret Passage to Room {0} and Make Suggestion...", GetRoomName());
        }
        return "Invalid Move ID";
    }

    private string GetRoomName()
    {
        return StandardValueRepository.Instance.GetLocationName((StandardEnums.LocationEnum)LocationID.Value);
    }
}
