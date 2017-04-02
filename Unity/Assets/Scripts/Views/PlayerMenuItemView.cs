using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuItemView : MonoBehaviour
{
    private Color DEFAULT_BACKGROUND_COLOR;
    private Color ELIMINATION_COLOR;
    private Color INACTIVE_COLOR;
    private const int INACTIVE_ALPHA = 80;
    private const int ACTIVE_ALPHA = 255;
    
    public Image BackgroundImage;
    public Image PlayerPortrait;
    public Text PlayerName;
    public Text PlayerNote;
    private PlayerMenuItemViewModel playerViewModel;

    void Start()
    {
        this.DEFAULT_BACKGROUND_COLOR = new Color(204, 232, 255, ACTIVE_ALPHA);
        this.ELIMINATION_COLOR = new Color(255, 0, 0, 100);
        this.INACTIVE_COLOR = new Color(DEFAULT_BACKGROUND_COLOR.r, DEFAULT_BACKGROUND_COLOR.g, DEFAULT_BACKGROUND_COLOR.b, INACTIVE_ALPHA);
    }

    public void BindViewModel(PlayerMenuItemViewModel viewModel)
    {
        this.playerViewModel = viewModel;
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        UpdateAllFromViewModel();
    }

    private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "IsAtTurn":
                UpdateIsAtTurn();
                break;

            case "IsEliminated":
                UpdateIsEliminated();
                break;

            case "IsActive":
                UpdateIsActive();
                break;

            case "PlayerNote":
                UpdatePlayerNote();
                break;

            case "PlayerName":
                UpdatePlayerName();
                break;

            case "PlayerPortrait":
                UpdatePlayerPortrait();
                break;

        }
    }

    private void UpdateAllFromViewModel()
    {
        UpdateIsActive();
        UpdateIsAtTurn();
        UpdateIsEliminated();
        UpdatePlayerName();
        UpdatePlayerNote();
        UpdatePlayerPortrait();
    }

    private void UpdatePlayerPortrait()
    {
        this.PlayerPortrait = playerViewModel.PlayerPortrait;
    }

    private void UpdatePlayerName()
    {
        this.PlayerName.text = playerViewModel.PlayerName;
    }

    private void UpdatePlayerNote()
    {
        this.PlayerNote.text = playerViewModel.PlayerNote;
    }

    private void UpdateIsActive()
    {
        this.BackgroundImage.color = playerViewModel.IsActive ? DEFAULT_BACKGROUND_COLOR : INACTIVE_COLOR;
    }

    private void UpdateIsEliminated()
    {
        this.BackgroundImage.color = playerViewModel.IsEliminated ? this.ELIMINATION_COLOR : this.DEFAULT_BACKGROUND_COLOR;
    }

    private void UpdateIsAtTurn()
    {
        this.PlayerNote.text = playerViewModel.IsAtTurn ? "At Turn!" : string.Empty;
    }
}
