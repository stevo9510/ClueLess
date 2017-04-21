using System;

[Serializable]
public class CardTypeTuple : BaseServerToClientMessage
{
    public StandardEnums.PlayerEnum playerID;
    public StandardEnums.WeaponEnum weaponID;
    public StandardEnums.RoomEnum roomID;
}
