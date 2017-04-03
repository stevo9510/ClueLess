using System.Collections.Generic;
using UnityEngine;

public class MoveMenuPresenter : MonoBehaviour {

    /// <summary>
    /// Prefab used to instantiate each Move Option in the list
    /// </summary>
    public GameObject MoveOptionPrefab;

    /// <summary>
    /// Host of Game Move Options
    /// </summary>
    public GameObject MoveOptionHost;

	// Use this for initialization
	void Start () {
        // Clear out move options on start.
        ClearAllMoveOptions();

        // TODO: Subscribe to server listener for when move options are available.

        // TODO: Test code.  Comment me out later
        var mockObject1 = new MoveModel() { MoveID = 1, LocationID = 2 };
        var mockObject2 = new MoveModel() { MoveID = 2, LocationID = 3 };
        AddMoveOptions(new List<MoveModel>() { mockObject1, mockObject2 });
    }

    /// <summary>
    /// Clear the move options
    /// </summary>
    private void ClearAllMoveOptions()
    {
        foreach (Transform child in MoveOptionHost.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Takes in a list of move options and instantiates move option controls for each move
    /// Wires up to move option button to handle move being made by player.
    /// </summary>
    /// <param name="moves"></param>
    private void AddMoveOptions(List<MoveModel> moves)
    {
        foreach (MoveModel move in moves)
        {
            GameObject moveOptionItem = Instantiate(MoveOptionPrefab);
            moveOptionItem.transform.SetParent(this.MoveOptionHost.transform, false);
            MoveOptionView moveViewModel = moveOptionItem.GetComponent<MoveOptionView>();
            moveViewModel.textBox.text = move.ToString();
            moveViewModel.button.onClick.AddListener(() => MoveButtonHandler(move));
        }
    }

    /// <summary>
    /// When move button is clicked, make the move for the player.
    /// </summary>
    /// <param name="movePerformed"></param>
    private void MoveButtonHandler(MoveModel movePerformed)
    {
        // TODO: Handle making game move.  Call to server to make game move

        
        ClearAllMoveOptions();
    } 
}

