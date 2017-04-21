/// <summary>
/// Message sent to server when connecting to the game initially.  
/// </summary>
public class ConnectToGameMessage : BaseClientToServerMessage
{
    public override StandardEnums.ClientToServerMessageEnum MessageID
    {
        get
        {
            return StandardEnums.ClientToServerMessageEnum.ConnectToGame;
        }
    }
}
