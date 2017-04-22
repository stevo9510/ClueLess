using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Presenter script that can be placed on any Card item list (e.g. choose a character, weapon, etc.)
/// This assumes that the child components have a button component that can be clicked, and a CardEnumTag
/// that defines the entity ID of that card.
/// </summary>
public class CardSelectorPresenter : MonoBehaviour {
    public event System.Action<int> CardSelected;

	// Use this for initialization
	void Start ()
    {
        // Wire up an OnClick listener to all card buttons 
        this.GetComponentsInChildren<Button>().ToList().ForEach(
            cardButton => cardButton.onClick.AddListener(() => ButtonHandler(cardButton)));
	}
	
    /// <summary>
    /// When a button is clicked, get its enum and fire an event to subscribers of the card that was clicked
    /// </summary>
    /// <param name="cardButton"></param>
    private void ButtonHandler(Button cardButton)
    {
        int entityID = cardButton.gameObject.GetComponent<CardEnumTag>().EntityID;
        var handler = CardSelected;
        if(handler != null)
        {
            handler(entityID);
        }
    }

}
