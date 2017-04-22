using System;

[Serializable]
public class SuggestionMoveMadeMessage : CardTypeTuple
{
    public StandardEnums.PlayerEnum playerMakingSuggestionID;
}
