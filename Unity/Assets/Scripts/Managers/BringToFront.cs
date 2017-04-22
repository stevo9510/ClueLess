using UnityEngine;

/// <summary>
/// Apply this script to an object to bring it to the front of the display (compared to all other siblings under the parent)
/// Happens when enabled
/// </summary>
public class BringToFront : MonoBehaviour {

	// Use this for initialization
    void OnEnable()
    {
        this.transform.SetAsLastSibling();
    }
}
