using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///  Game board UI View that handles moving players and weapons to locations 
/// </summary>
public class GameBoardView : MonoBehaviour {

    #region All Game Objects

    public GameObject hallwayStudyHall;
    public GameObject hallwayStudyLib;
    public GameObject hallwayHallLounge;
    public GameObject hallwayHallBill;
    public GameObject hallwayLoungeDin;
    public GameObject hallwayLibBill;
    public GameObject hallwayBillDin;
    public GameObject hallwayLibCon;
    public GameObject hallwayConBall;
    public GameObject hallwayBillBall;
    public GameObject hallwayDinKitch;
    public GameObject hallwayBallKitch;
    public GameObject roomStudy;
    public GameObject roomHall;
    public GameObject roomLounge;
    public GameObject roomLibrary;
    public GameObject roomBilliard;
    public GameObject roomDining;
    public GameObject roomConservatory;
    public GameObject roomBallRoom;
    public GameObject roomKitchen;
    public GameObject gamePieceMsScarlet;
    public GameObject gamePieceColMustard;
    public GameObject gamePieceDrOrchid;
    public GameObject gamePieceMrsPeacock;
    public GameObject gamePieceMrGreen;
    public GameObject gamePieceProfPlum;
    public GameObject weaponCandlestick;
    public GameObject weaponLeadpipe;
    public GameObject weaponDagger;
    public GameObject weaponRevolver;
    public GameObject weaponRope;
    public GameObject weaponWrench;

    #endregion

    private Dictionary<StandardEnums.LocationEnum, GameObject> locationObjects;
    private Dictionary<StandardEnums.PlayerEnum, GameObject> playerGamePieces;
    private Dictionary<StandardEnums.WeaponEnum, GameObject> weaponGamePieces;
    private System.Random randomNumberGenerator = new System.Random();

    private List<RoomSubPosition> roomSubPositionsForPlayers = new List<RoomSubPosition>();
    private List<RoomSubPosition> roomSubPositionsForWeapons = new List<RoomSubPosition>();

    private IServerToClientMessagePublisher messagePublisher;

    const int ROOM_SUB_POSITIONS = 7;

    // Use this for initialization
    void Start ()
    {

        messagePublisher = Network.Instance;
        messagePublisher.EventPlayerMoved += MessagePublisher_PlayerMoved;
        messagePublisher.EventWeaponMoved += MessagePublisher_WeaponMoved;

        locationObjects = new Dictionary<StandardEnums.LocationEnum, GameObject>();
        playerGamePieces = new Dictionary<StandardEnums.PlayerEnum, GameObject>();
        weaponGamePieces = new Dictionary<StandardEnums.WeaponEnum, GameObject>();

        locationObjects[StandardEnums.LocationEnum.BallKitch] = hallwayBallKitch;
        locationObjects[StandardEnums.LocationEnum.Ballroom] = roomBallRoom;
        locationObjects[StandardEnums.LocationEnum.BillBall] = hallwayBillBall;
        locationObjects[StandardEnums.LocationEnum.BillDin] = hallwayBillDin;
        locationObjects[StandardEnums.LocationEnum.BilliardRoom] = roomBilliard;
        locationObjects[StandardEnums.LocationEnum.ConBall] = hallwayConBall;
        locationObjects[StandardEnums.LocationEnum.Conservatory] = roomConservatory;
        locationObjects[StandardEnums.LocationEnum.DiningRoom] = roomDining;
        locationObjects[StandardEnums.LocationEnum.DinKitch] = hallwayDinKitch;
        locationObjects[StandardEnums.LocationEnum.Hall] = roomHall;
        locationObjects[StandardEnums.LocationEnum.HallBill] = hallwayHallBill;
        locationObjects[StandardEnums.LocationEnum.HallLounge] = hallwayHallLounge;
        locationObjects[StandardEnums.LocationEnum.Kitchen] = roomKitchen;
        locationObjects[StandardEnums.LocationEnum.LibBill] = hallwayLibBill;
        locationObjects[StandardEnums.LocationEnum.LibCon] = hallwayLibCon;
        locationObjects[StandardEnums.LocationEnum.Library] = roomLibrary;
        locationObjects[StandardEnums.LocationEnum.Lounge] = roomLounge;
        locationObjects[StandardEnums.LocationEnum.LoungeDin] = hallwayLoungeDin;
        locationObjects[StandardEnums.LocationEnum.Study] = roomStudy;
        locationObjects[StandardEnums.LocationEnum.StudyHall] = hallwayStudyHall;
        locationObjects[StandardEnums.LocationEnum.StudyLib] = hallwayStudyLib;

        playerGamePieces[StandardEnums.PlayerEnum.Green] = gamePieceMrGreen;
        playerGamePieces[StandardEnums.PlayerEnum.Mustard] = gamePieceColMustard;
        playerGamePieces[StandardEnums.PlayerEnum.Orchid] = gamePieceDrOrchid;
        playerGamePieces[StandardEnums.PlayerEnum.Peacock] = gamePieceMrsPeacock;
        playerGamePieces[StandardEnums.PlayerEnum.Plum] = gamePieceProfPlum;
        playerGamePieces[StandardEnums.PlayerEnum.Scarlet] = gamePieceMsScarlet;

        weaponGamePieces[StandardEnums.WeaponEnum.Candlestick] = weaponCandlestick;
        weaponGamePieces[StandardEnums.WeaponEnum.Dagger] = weaponDagger;
        weaponGamePieces[StandardEnums.WeaponEnum.LeadPipe] = weaponLeadpipe;
        weaponGamePieces[StandardEnums.WeaponEnum.Revolver] = weaponRevolver;
        weaponGamePieces[StandardEnums.WeaponEnum.Rope] = weaponRope;
        weaponGamePieces[StandardEnums.WeaponEnum.Wrench] = weaponWrench;
        InitializeRoomSubPositions(roomSubPositionsForPlayers);
        InitializeRoomSubPositions(roomSubPositionsForWeapons);
    }

    /// <summary>
    /// Player Moved message handler
    /// </summary>
    /// <param name="msg"></param>
    private void MessagePublisher_PlayerMoved(PlayerMovedMessage msg)
    {
        MovePlayer(msg.playerID, msg.locationID, roomSubPositionsForPlayers);
    }

    /// <summary>
    /// Weapon Moved message handler
    /// </summary>
    /// <param name="msg"></param>
    private void MessagePublisher_WeaponMoved(WeaponMovedMessage msg)
    {
        MoveWeapon(msg.weaponID, msg.locationID, roomSubPositionsForWeapons);
    }

    /// <summary>
    /// Move a player to a location 
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="locID"></param>
    private void MovePlayer(StandardEnums.PlayerEnum playerID, StandardEnums.LocationEnum locID, 
        List<RoomSubPosition> roomSubPositions)
    {
        // get the current position
        var currentSubPos = roomSubPositions.Where(rsp => rsp.entityID == (int)playerID).FirstOrDefault();
        if(currentSubPos != null)
        {
            currentSubPos.entityID = null;
        }

        RoomSubPosition newSubPos = GetNextMoveToSubPositionObject((int)playerID, locID, roomSubPositions);

        var playerTransform = playerGamePieces[playerID].transform;

        // set the player transform = the locationTransform (plus a bit of a forward offset
        Transform locationTransform = locationObjects[locID].transform;
        playerTransform.position = locationTransform.position + (-1.0F * locationTransform.forward);

        // if in a room, offset based on the new sub position of the room (to help avoid collisions with objects)
        if (newSubPos != null && newSubPos.subPositionID != 0)
        {
            const float playerDegreeOffset = 30.0F; // players have a 30 degree offset from weapons to try and avoid some collisions
            int plusOrMinus = ((int)locID % 2 == 0) ? 1 : -1;
            var degreesToRotate = (newSubPos.subPositionID - 1) * 60.0F + playerDegreeOffset;
            TransformTargetToBeOffsetFromSourceByDegrees(playerTransform, locationTransform, plusOrMinus, degreesToRotate);
        }
    }

    /// <summary>
    /// Move a weapon to a location
    /// </summary>
    /// <param name="weaponID"></param>
    /// <param name="locID"></param>
    private void MoveWeapon(StandardEnums.WeaponEnum weaponID, StandardEnums.LocationEnum locID, List<RoomSubPosition> roomSubPositions)
    {
        var currentSubPos = roomSubPositions.Where(rsp => rsp.entityID == (int)weaponID).FirstOrDefault();
        if (currentSubPos != null)
        {
            currentSubPos.entityID = null;
        }

        RoomSubPosition newSubPos = GetNextMoveToSubPositionObject((int)weaponID, locID, roomSubPositions);
        var weaponTransform = weaponGamePieces[weaponID].transform;
        MoveTransformToLocation(weaponTransform, locID);
        Transform locationTransform = locationObjects[locID].transform;
        weaponTransform.position = locationTransform.position + (-1.0F * locationTransform.forward);

        weaponTransform.SetAsFirstSibling();

        if(newSubPos != null)
        {
            //var degreesOfFreedom = ROOM_SUB_POSITIONS - 1;
            int plusOrMinus = ((int)locID % 2 == 1) ? 1 : -1;
            var degreesToRotate = newSubPos.subPositionID * 60.0F;
            TransformTargetToBeOffsetFromSourceByDegrees(weaponTransform, locationTransform, plusOrMinus, degreesToRotate);
        }
    }

    private static void TransformTargetToBeOffsetFromSourceByDegrees(Transform targetToTransform,
        Transform sourceTransform, int plusOrMinus, float degreesToRotate)
    {
        var radians = Math.PI * degreesToRotate / 180.0F;
        var upDistance = (float)Math.Sin(radians);
        var rightDistance = (float)Math.Cos(radians);
        targetToTransform.position = targetToTransform.position +
            (upDistance * sourceTransform.up * 2.0F * plusOrMinus) +
            (rightDistance * sourceTransform.right * 2.0F * plusOrMinus);
    }

    private static RoomSubPosition GetNextMoveToSubPositionObject(int entityID, StandardEnums.LocationEnum locID, List<RoomSubPosition> roomSubPositions)
    {
        RoomSubPosition newSubPos = null;
        if (StandardValueRepository.Instance.RoomToLocationEnumMapping.ContainsValue(locID))
        {
            var roomID = StandardValueRepository.Instance.RoomToLocationEnumMapping.Where(kvp => kvp.Value == locID).Select(kvp => kvp.Key).First();
            newSubPos = roomSubPositions.Where(rsp => rsp.roomID == roomID && !rsp.entityID.HasValue).FirstOrDefault();
            if (newSubPos != null)
            {
                newSubPos.entityID = entityID;
            }
        }
        return newSubPos;
    }

    /// <summary>
    /// Move a general transform to a specified location.  If it is a room being moved to, then 
    /// a random direction is used to try and avoid collisions of pieces.  
    /// TODO: May come up with a better, more complex solution if more time permits
    /// </summary>
    /// <param name="transformToMove"></param>
    /// <param name="locID"></param>
    private void MoveTransformToLocation(Transform transformToMove, StandardEnums.LocationEnum locID)
    {

        // this moves the object to a random direction within the location if that location is a room
        //if (Enum.GetValues(typeof(StandardEnums.RoomEnum)).Cast<int>().Contains((int)locID))
        //{
        //    int plusOrMinusRight = randomNumberGenerator.NextDouble() > 0.5 ? 1 : -1;
        //    int plusOrMinusUp = randomNumberGenerator.NextDouble() > 0.5 ? 1 : -1;
        //    transformToMove.position = transformToMove.position +
        //        (2.0F * locationTransform.right * (float)randomNumberGenerator.NextDouble() * plusOrMinusRight) +
        //        (2.0F * locationTransform.up * (float)randomNumberGenerator.NextDouble() * plusOrMinusUp);
        //}
    }

    /// <summary>
    /// Initialize the room sub positions 
    /// </summary>
    /// <param name="roomSubPositionCollection"></param>
    private void InitializeRoomSubPositions(List<RoomSubPosition> roomSubPositionCollection)
    {
        foreach (var roomID in Enum.GetValues(typeof(StandardEnums.RoomEnum)))
        {
            for (int index = 0; index < ROOM_SUB_POSITIONS; index++)
            {
                var roomSubPos = new RoomSubPosition((StandardEnums.RoomEnum)roomID, index);
                roomSubPositionCollection.Add(roomSubPos);
            }
        }
    }
    
    /// <summary>
    /// Class to handle storing entity (either weapon or player) locations with their sub position location
    /// </summary>
    public class RoomSubPosition
    {
        public StandardEnums.RoomEnum roomID;
        public int subPositionID;
        public int? entityID;

        public RoomSubPosition(StandardEnums.RoomEnum rID, int pos)
        {
            this.roomID = rID;
            this.subPositionID = pos;
        }
    }
}
