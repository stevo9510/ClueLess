namespace Assets.Scripts.Models
{
    public class MoveMessage
    {
        public StandardEnums.MoveEnum MoveID { get; set; }

        public StandardEnums.WeaponEnum? WeaponID { get; set; }

        public StandardEnums.LocationEnum? LocationID { get; set; }

        public StandardEnums.PlayerEnum? PlayerID { get; set; }
    }
}
