using System;

[Serializable]
public class SuggestionProveTurnMessage : BaseServerToClientMessage
{
    public StandardEnums.PlayerEnum playerID;
}
