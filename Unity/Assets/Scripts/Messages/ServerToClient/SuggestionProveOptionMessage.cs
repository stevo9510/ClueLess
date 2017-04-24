using System;
using System.Collections.Generic;

[Serializable]
public class SuggestionProveOptionMessage : BaseServerToClientMessage
{
    public int dummyVal;
    public List<StandardEnums.CardEnum> cardsPlayerCanSelect;
}
