using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour {

	// Use this for initialization
    void OnEnable()
    {
        this.transform.SetAsLastSibling();
    }
}
