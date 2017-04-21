/// <summary>
/// Message sent from client to server when making a game move
/// </summary>
public class MoveMessage : BaseClientToServerMessage
{
    /// <summary>
    /// The type of Move that is being made (e.g. move to hallway, move to room and suggestion, make accusation, etc.)
    /// </summary>
    public StandardEnums.MoveEnum MoveID { get; set; }

    /// <summary>
    /// This can be the following (and possibly both 1 & 2, depending on if moving to a room):
    /// 1. The location the player is moving to.
    /// 2. The location (room) that is being suggestion [which is always where the player is moving to].
    /// 3. The location that is being accused
    /// </summary>
    public StandardEnums.LocationEnum? LocationID { get; set; }

    /// <summary>
    /// The weapon that is being accused or suggested (if applicable)
    /// </summary>
    public StandardEnums.WeaponEnum? WeaponID { get; set; }

    /// <summary>
    /// The player that is being accused or suggested (if applicable)
    /// </summary>
    public StandardEnums.PlayerEnum? PlayerID { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="pMoveID"></param>
    public MoveMessage(StandardEnums.MoveEnum pMoveID, StandardEnums.LocationEnum? pLocID)
    {
        this.MoveID = pMoveID;
        this.LocationID = pLocID;
    }
}
