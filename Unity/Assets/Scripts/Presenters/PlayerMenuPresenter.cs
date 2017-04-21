using UnityEngine;

public class PlayerMenuPresenter : MonoBehaviour {
    public PlayerMenuItemView player1View;
    public PlayerMenuItemView player2View;
    public PlayerMenuItemView player3View;
    public PlayerMenuItemView player4View;
    public PlayerMenuItemView player5View;
    public PlayerMenuItemView player6View;

    private PlayerMenuItemViewModel player1ViewModel;
    private PlayerMenuItemViewModel player2ViewModel;
    private PlayerMenuItemViewModel player3ViewModel;
    private PlayerMenuItemViewModel player4ViewModel;
    private PlayerMenuItemViewModel player5ViewModel;
    private PlayerMenuItemViewModel player6ViewModel;

    // Use this for initialization
    void Start ()
    {
        // Create all the ViewModels 
        player1ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Scarlet);
        player2ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Mustard);
        player3ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Orchid);
        player4ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Green);
        player5ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Peacock);
        player6ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Plum);

        player1View.BindViewModel(player1ViewModel);
        player2View.BindViewModel(player2ViewModel);
        player3View.BindViewModel(player3ViewModel);
        player4View.BindViewModel(player4ViewModel);
        player5View.BindViewModel(player5ViewModel);
        player6View.BindViewModel(player6ViewModel);

        StandardValueRepository.Instance.PlayerDetailsChanged += Instance_PlayerDetailsChanged;
    }

    private void Instance_PlayerDetailsChanged(object sender, System.EventArgs e)
    {
        // TODO: Update all player names here.    
    }

    private PlayerMenuItemViewModel CreateDefaultViewModel(StandardEnums.PlayerEnum playerID)
    {
        var viewModel = new PlayerMenuItemViewModel(playerID, StandardValueRepository.Instance.GetPlayerName(playerID));
        return viewModel;
    }

}
