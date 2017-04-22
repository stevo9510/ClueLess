using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuItemView : MonoBehaviour
{
    private Color DEFAULT_BACKGROUND_COLOR;
    private Color ELIMINATION_COLOR;
    private Color INACTIVE_COLOR;
    private const float INACTIVE_ALPHA = 0.60F;
    private const float ACTIVE_ALPHA = 1.0F;
    
    public Image BackgroundImage;
    public Image PlayerPortrait;
    public Text PlayerName;
    public Text PlayerNote;
    private PlayerMenuItemViewModel playerViewModel;

    private void Awake()
    {
        this.DEFAULT_BACKGROUND_COLOR = new Color(204.0F/255.0F, 232.0F/255.0F, 255.0F/255.0F, ACTIVE_ALPHA);
        this.ELIMINATION_COLOR = new Color(1, 0, 0, 0.4F);
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
            case "IsClient":
                UpdateIsClient();
                break;

            case "IsWinner":
                UpdateIsWinner();
                break;

            case "IsAtTurn":
                UpdateIsAtTurn();
                break;

            case "IsEliminated":
                UpdateIsEliminated();
                break;

            case "IsActive":
                UpdateIsActive();
                break;

            case "PlayerName":
                UpdatePlayerName();
                break;

            case "PlayerPortrait":
                UpdatePlayerPortrait();
                break;
        }
        UpdatePlayerNote();
    }

    private void UpdateAllFromViewModel()
    {
        UpdateIsAtTurn();
        UpdateIsEliminated();
        UpdateIsWinner();
        UpdateIsActive();
        UpdatePlayerName();
        UpdatePlayerPortrait();
        UpdatePlayerNote();
    }

    private void UpdateIsWinner()
    {
    }

    private void UpdateIsClient()
    {
    }

    private void UpdatePlayerPortrait()
    {
        this.PlayerPortrait = playerViewModel.PlayerPortrait;
    }

    private void UpdatePlayerName()
    {
        this.PlayerName.text = playerViewModel.PlayerName;
    }

    private void UpdateIsActive()
    {
        UpdateBackgroundImage();
    }

    private void UpdateIsEliminated()
    {
        UpdateBackgroundImage();
    }

    private void UpdateBackgroundImage()
    {
        Color backgroundColor = DEFAULT_BACKGROUND_COLOR;
        if(!playerViewModel.IsActive)
        {
            backgroundColor = INACTIVE_COLOR;
        }
        else if(playerViewModel.IsEliminated)
        {
            backgroundColor = ELIMINATION_COLOR;
        }
        this.BackgroundImage.color = backgroundColor;
    }

    private void UpdateIsAtTurn()
    {
    }

    private void UpdatePlayerNote()
    {
        if(this.playerViewModel.IsWinner)
        {
            this.PlayerNote.text = "Winner!";
        }
        else if(this.playerViewModel.IsEliminated)
        {
            this.PlayerNote.text = "Eliminated!";
        }
        else if(this.playerViewModel.IsAtTurn)
        {
            this.PlayerNote.text = "Current Turn!";
        }
        else if(this.playerViewModel.IsClient)
        {
            this.PlayerNote.text = "You";
        }
        else
        {
            this.PlayerNote.text = string.Empty;
        }
    }

}
