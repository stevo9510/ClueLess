using System;
using System.Collections.Generic;

[Serializable]
public class PlayersInGameMessage : BaseServerToClientMessage
{
    public List<StandardEnums.PlayerEnum> playerIDs;
}
