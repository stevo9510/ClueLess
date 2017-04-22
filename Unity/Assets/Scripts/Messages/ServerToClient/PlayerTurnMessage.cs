using System;

[Serializable]
public class PlayerTurnMessage : BaseServerToClientMessage
{
    public StandardEnums.PlayerEnum playerID;
}
