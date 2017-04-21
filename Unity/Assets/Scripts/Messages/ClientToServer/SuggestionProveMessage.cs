/// <summary>
/// Message sent from client to server when proving a suggestion.  
/// </summary>
public class SuggestionProveMessage : BaseClientToServerMessage
{
    public StandardEnums.CardEnum? CardID;

    /// <summary>
    /// Pass null if no card can be placed to prove suggestion wrong, otherwise, pass the CardID that proves the suggestion wrong
    /// </summary>
    /// <param name="pCardID"></param>
    public SuggestionProveMessage(StandardEnums.CardEnum? pCardID)
    {
        this.CardID = pCardID;
    }

}
