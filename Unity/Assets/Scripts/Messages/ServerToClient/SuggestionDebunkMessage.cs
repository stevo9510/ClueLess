using System;

[Serializable]
public class SuggestionDebunkMessage : BaseServerToClientMessage
{
    public StandardEnums.PlayerEnum playerID;
    public StandardEnums.CardEnum cardID;
}
