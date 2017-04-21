/// <summary>
/// Base Client to Server Message
/// </summary>
public abstract class BaseClientToServerMessage
{
    public abstract StandardEnums.ClientToServerMessageEnum MessageID { get; }
}
