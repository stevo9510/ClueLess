using System;
using System.Collections.Generic;

[Serializable]
public class MoveOptionMessage : BaseServerToClientMessage
{
    public List<MoveOption> moveOptions;
}

[Serializable]
public class MoveOption
{
    public StandardEnums.MoveEnum moveID;
    public StandardEnums.LocationEnum locationID;
}
