using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ViewCardPresenter : MonoBehaviour {

    public GameObject CardItemHost;
    public Text TextTitle;
    public Button ViewCardButton;
    public Button BackButton;
    public GameObject CardPrefab;

    private Dictionary<StandardEnums.CardEnum, GameObject> instantiatedCardButtons;
    private Vector3 cameraDefaultPosition;

    private enum PresenterMode
    {
        ViewCards,
        PickSuggestionProve
    }

    private PresenterMode _currentMode;
    private PresenterMode CurrentMode
    {
        get
        {
            return _currentMode;
        }
        set
        {
            _currentMode = value;
            if(value == PresenterMode.PickSuggestionProve)
            {
                this.TextTitle.text = "Choose a Card to Prove Suggestion Wrong";
                this.ViewCardButton.GetComponentInChildren<Text>().text = "Choose Card...";
            }
            else if(value == PresenterMode.ViewCards)
            {
                this.TextTitle.text = "Player Dealt Cards";
                this.ViewCardButton.GetComponentInChildren<Text>().text = "View Cards...";
                SetAllActive();
            }
        }
    }

	// Use this for initialization
	void Start () {
        instantiatedCardButtons = new Dictionary<StandardEnums.CardEnum, GameObject>();
        ViewCardButton.onClick.AddListener(new UnityEngine.Events.UnityAction(ViewCardButtonClick));
        BackButton.onClick.AddListener(new UnityEngine.Events.UnityAction(BackButtonClick));
        this.cameraDefaultPosition = Camera.main.transform.position;
        ClearAllCardItems();
        ViewCardButton.enabled = false;

        // TODO: Comment out later because it will handled by the server
        HandleGameCardsDealt(new List<StandardEnums.CardEnum>() { StandardEnums.CardEnum.Hall, StandardEnums.CardEnum.Dagger, StandardEnums.CardEnum.Orchid });
    }

    private void ViewCardButtonClick()
    {
        MoveCameraToViewCards();
    }

    #region Server call handlers

    private void HandleGameCardsDealt(List<StandardEnums.CardEnum> cards)
    {
        foreach (StandardEnums.CardEnum cardID in cards)
        {
            GameObject instantiatedCard = Instantiate(CardPrefab);
            instantiatedCard.transform.SetParent(this.CardItemHost.transform, false);
            Button cardButton = instantiatedCard.gameObject.GetComponent<Button>();
            cardButton.gameObject.GetComponent<Image>().sprite = StandardValueRepository.Instance.GetCardSprite(cardID);
            instantiatedCardButtons[cardID] = instantiatedCard;
            cardButton.onClick.AddListener(() => OnCardClicked(cardID));
        }

        ViewCardButton.enabled = true;
        this.CurrentMode = PresenterMode.ViewCards;
    }

    private void HandleDebunkSelection(List<StandardEnums.CardEnum> cardEnums)
    {
        this.CurrentMode = PresenterMode.PickSuggestionProve;
        SetAllActive();
        this.instantiatedCardButtons.Where(kvp => !cardEnums.Contains(kvp.Key)).ToList().ForEach(item => item.Value.SetActive(false));
    }

    #endregion

    private void SetAllActive()
    {
        this.instantiatedCardButtons.Values.ToList().ForEach(item => item.SetActive(true));
    }

    private void BackButtonClick()
    {
        MoveCameraBackToDefaultLocation();
    }

    #region Move Camera Actions

    private void MoveCameraToViewCards()
    {
        Camera.main.transform.position = CardItemHost.transform.position + (CardItemHost.transform.forward * -15.0F);
    }

    private void MoveCameraBackToDefaultLocation()
    {
        Camera.main.transform.position = this.cameraDefaultPosition;
    }

    #endregion

    private void OnCardClicked(StandardEnums.CardEnum cardEnum)
    {
        if (this.CurrentMode == PresenterMode.PickSuggestionProve)
        {
            // TODO: Send message to server

            this.CurrentMode = PresenterMode.ViewCards;
            MoveCameraBackToDefaultLocation();
        }
    }

    private void ClearAllCardItems()
    {
        foreach (Transform child in CardItemHost.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
