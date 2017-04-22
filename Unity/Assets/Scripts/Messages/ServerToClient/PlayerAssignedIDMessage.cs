using System;

[Serializable]
public class PlayerAssignedIDMessage : BaseServerToClientMessage
{
    public StandardEnums.PlayerEnum playerID;
}
