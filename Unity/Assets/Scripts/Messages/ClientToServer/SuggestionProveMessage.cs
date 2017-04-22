using System;
/// <summary>
/// Message sent from client to server when proving a suggestion.  
/// </summary>
[Serializable]
public class SuggestionProveMessage : BaseClientToServerMessage
{
    public StandardEnums.CardEnum CardID;

    /// <summary>
    /// Use this if no option is available to prove the suggestion wrong
    /// </summary>
    public SuggestionProveMessage()
    {

    }

    /// <summary>
    /// Use this to pass the card to prove the suggestion wrong
    /// </summary>
    /// <param name="pCardID"></param>
    public SuggestionProveMessage(StandardEnums.CardEnum pCardID)
    {
        this.CardID = pCardID;
    }

}
