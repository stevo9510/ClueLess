using System;

[Serializable]
public class WeaponMovedMessage : BaseServerToClientMessage
{
    public StandardEnums.WeaponEnum weaponID;
    public StandardEnums.LocationEnum locationID;
}