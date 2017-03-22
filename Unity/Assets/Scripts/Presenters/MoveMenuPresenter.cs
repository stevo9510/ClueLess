using System.Collections.Generic;
using UnityEngine;

public class MoveMenuController : MonoBehaviour {

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
        RemoveChildren();

        // TODO: Test code.  Comment me out later
        var mockObject1 = new MoveModel() { MoveID = 1, LocationID = 2 };
        var mockObject2 = new MoveModel() { MoveID = 2, LocationID = 3 };
        AddChildren(new List<MoveModel>() { mockObject1, mockObject2 });
    }

    private void RemoveChildren()
    {
        foreach (Transform child in MoveOptionHost.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddChildren(List<MoveModel> moves)
    {
        foreach (MoveModel move in moves)
        {
            GameObject moveOptionItem = Instantiate(MoveOptionPrefab);
            moveOptionItem.transform.SetParent(this.MoveOptionHost.transform, false);
            MoveOptionViewModel moveViewModel = moveOptionItem.GetComponent<MoveOptionViewModel>();
            moveViewModel.textBox.text = move.ToString();
            moveViewModel.button.onClick.AddListener(() => MoveHandler(move));
        }
    }

    private void MoveHandler(MoveModel movePerformed)
    {

    } 
}

