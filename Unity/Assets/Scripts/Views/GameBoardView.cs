using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameBoardView : MonoBehaviour {
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

    private Dictionary<StandardEnums.LocationEnum, GameObject> locations;
    private Dictionary<StandardEnums.PlayerEnum, GameObject> playerGamePieces;
    private Dictionary<StandardEnums.WeaponEnum, GameObject> weaponGamePieces;
    private System.Random rand = new System.Random();


    // Use this for initialization
    void Start () {
        locations = new Dictionary<StandardEnums.LocationEnum, GameObject>();
        playerGamePieces = new Dictionary<StandardEnums.PlayerEnum, GameObject>();
        weaponGamePieces = new Dictionary<StandardEnums.WeaponEnum, GameObject>();

        locations[StandardEnums.LocationEnum.BallKitch] = hallwayBallKitch;
        locations[StandardEnums.LocationEnum.Ballroom] = roomBallRoom;
        locations[StandardEnums.LocationEnum.BillBall] = hallwayBillBall;
        locations[StandardEnums.LocationEnum.BillDin] = hallwayBillDin;
        locations[StandardEnums.LocationEnum.BilliardRoom] = roomBilliard;
        locations[StandardEnums.LocationEnum.ConBall] = hallwayConBall;
        locations[StandardEnums.LocationEnum.Conservatory] = roomConservatory;
        locations[StandardEnums.LocationEnum.DiningRoom] = roomDining;
        locations[StandardEnums.LocationEnum.DinKitch] = hallwayDinKitch;
        locations[StandardEnums.LocationEnum.Hall] = roomHall;
        locations[StandardEnums.LocationEnum.HallBill] = hallwayHallBill;
        locations[StandardEnums.LocationEnum.HallLounge] = hallwayHallLounge;
        locations[StandardEnums.LocationEnum.Kitchen] = roomKitchen;
        locations[StandardEnums.LocationEnum.LibBill] = hallwayLibBill;
        locations[StandardEnums.LocationEnum.LibCon] = hallwayLibCon;
        locations[StandardEnums.LocationEnum.Library] = roomLibrary;
        locations[StandardEnums.LocationEnum.Lounge] = roomLounge;
        locations[StandardEnums.LocationEnum.LoungeDin] = hallwayLoungeDin;
        locations[StandardEnums.LocationEnum.Study] = roomStudy;
        locations[StandardEnums.LocationEnum.StudyHall] = hallwayStudyHall;
        locations[StandardEnums.LocationEnum.StudyLib] = hallwayStudyLib;

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

    private void MovePlayer(StandardEnums.PlayerEnum playerID, StandardEnums.LocationEnum locID)
    {
        MoveTransformToLocation(playerGamePieces[playerID].transform, locID);
    }

    private void MoveWeapon(StandardEnums.WeaponEnum weaponID, StandardEnums.LocationEnum locID)
    {
        MoveTransformToLocation(weaponGamePieces[weaponID].transform, locID);
    }


    private void MoveTransformToLocation(Transform transformToMove, StandardEnums.LocationEnum locID)
    {
        Transform locationTransform = locations[locID].transform;
        transformToMove.position = locationTransform.position + (-1.0F * locationTransform.forward);

        // this moves the object to a random direction within the location if that location is a room
        if (Enum.GetValues(typeof(StandardEnums.RoomEnum)).Cast<int>().Contains((int)locID))
        {
            int plusOrMinusRight = rand.NextDouble() > 0.5 ? 1 : -1;
            int plusOrMinusUp = rand.NextDouble() > 0.5 ? 1 : -1;
            transformToMove.position = transformToMove.position +
                (2.0F * locationTransform.right * (float)rand.NextDouble() * plusOrMinusRight) +
                (2.0F * locationTransform.up * (float)rand.NextDouble() * plusOrMinusUp);
        }
    }


}
