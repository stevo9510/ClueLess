using System;

[Serializable]
public class PlayerMovedMessage : BaseServerToClientMessage
{
    public StandardEnums.PlayerEnum playerID;
    public StandardEnums.LocationEnum locationID;
}
