using UnityEngine;

/// <summary>
/// This is a simple script used to tag Cards (for views that use the CardSelectorPresenter) with the EntityID
/// The EntityID should be the actual card enum (as an int, so this can be reused across multiple places)
/// </summary>
public class CardEnumTag : MonoBehaviour
{
    [Tooltip("This should be the ID of the actual card type, e.g. WeaponID, PlayerID, RoomID")]
    public int EntityID;
}
