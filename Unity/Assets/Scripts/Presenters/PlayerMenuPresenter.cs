using UnityEngine;
using UnityEngine.UI;

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
        player1ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Scarlet, null);
        player2ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Mustard, null);
        player3ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Orchid, null);
        player4ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Green, null);
        player5ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Peacock, null);
        player6ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Plum, null);

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

    private PlayerMenuItemViewModel CreateDefaultViewModel(StandardEnums.PlayerEnum playerID, Image portrait)
    {
        player1ViewModel = new PlayerMenuItemViewModel(playerID, StandardValueRepository.Instance.GetPlayerName(playerID), portrait);
        return player1ViewModel;
    }

    // Update is called once per frame
    void Update ()
    {
        
	}
}
