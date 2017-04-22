using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private IServerToClientMessagePublisher messagePublisher;

    // Use this for initialization
    void Start () {

        // TODO: Replace this with actual Socket.IO publisher later. 
        messagePublisher = new StubServerToClientMessagePublisher();
        messagePublisher.PlayerMoved += MessagePublisher_PlayerMoved;
        messagePublisher.WeaponMoved += MessagePublisher_WeaponMoved;

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

        // Testing
        //MoveWeapon(StandardEnums.WeaponEnum.Candlestick, StandardEnums.LocationEnum.Ballroom);
        //MoveWeapon(StandardEnums.WeaponEnum.Dagger, StandardEnums.LocationEnum.Ballroom);

        //MovePlayer(StandardEnums.PlayerEnum.Green, StandardEnums.LocationEnum.BillBall);
        //MovePlayer(StandardEnums.PlayerEnum.Scarlet, StandardEnums.LocationEnum.BilliardRoom);

    }

    /// <summary>
    /// Player Moved message handler
    /// </summary>
    /// <param name="msg"></param>
    private void MessagePublisher_PlayerMoved(PlayerMovedMessage msg)
    {
        MovePlayer(msg.playerID, msg.locationID);
    }

    /// <summary>
    /// Weapon Moved message handler
    /// </summary>
    /// <param name="msg"></param>
    private void MessagePublisher_WeaponMoved(WeaponMovedMessage msg)
    {
        MoveWeapon(msg.weaponID, msg.locationID);
    }

    /// <summary>
    /// Move a player to a location 
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="locID"></param>
    private void MovePlayer(StandardEnums.PlayerEnum playerID, StandardEnums.LocationEnum locID)
    {
        MoveTransformToLocation(playerGamePieces[playerID].transform, locID);
    }

    /// <summary>
    /// Move a weapon to a location
    /// </summary>
    /// <param name="weaponID"></param>
    /// <param name="locID"></param>
    private void MoveWeapon(StandardEnums.WeaponEnum weaponID, StandardEnums.LocationEnum locID)
    {
        MoveTransformToLocation(weaponGamePieces[weaponID].transform, locID);
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
        Transform locationTransform = locationObjects[locID].transform;
        transformToMove.position = locationTransform.position + (-1.0F * locationTransform.forward);

        // this moves the object to a random direction within the location if that location is a room
        if (Enum.GetValues(typeof(StandardEnums.RoomEnum)).Cast<int>().Contains((int)locID))
        {
            int plusOrMinusRight = randomNumberGenerator.NextDouble() > 0.5 ? 1 : -1;
            int plusOrMinusUp = randomNumberGenerator.NextDouble() > 0.5 ? 1 : -1;
            transformToMove.position = transformToMove.position +
                (2.0F * locationTransform.right * (float)randomNumberGenerator.NextDouble() * plusOrMinusRight) +
                (2.0F * locationTransform.up * (float)randomNumberGenerator.NextDouble() * plusOrMinusUp);
        }
    }


}
