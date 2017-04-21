using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectorPresenter : MonoBehaviour {

    public event System.Action<int> CardSelected;

	// Use this for initialization
	void Start () {
        this.GetComponentsInChildren<Button>().ToList().ForEach(
            cardButton => cardButton.onClick.AddListener(() => ButtonHandler(cardButton)));
	}
	
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
