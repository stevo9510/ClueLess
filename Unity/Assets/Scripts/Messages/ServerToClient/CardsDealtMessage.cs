using System;
using System.Collections.Generic;

[Serializable]
public class CardsDealtMessage : BaseServerToClientMessage
{
    public List<StandardEnums.CardEnum> cardIDs;
}
