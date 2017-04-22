using System;
using System.Collections.Generic;

[Serializable]
public class SuggestionProveOptionMessage : BaseServerToClientMessage
{
    public List<StandardEnums.CardEnum> cardsPlayerCanSelect;
}
