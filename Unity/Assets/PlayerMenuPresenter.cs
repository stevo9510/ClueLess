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
    void Start () {
        player1ViewModel = new PlayerMenuItemViewModel(StandardEnums.PlayerEnum.Scarlet, "Miss Scarlet", null);
        player2ViewModel = new PlayerMenuItemViewModel(StandardEnums.PlayerEnum.Mustard, "Col. Mustard", null);
        player3ViewModel = new PlayerMenuItemViewModel(StandardEnums.PlayerEnum.Orchid, "Dr. Orchid", null);
        player4ViewModel = new PlayerMenuItemViewModel(StandardEnums.PlayerEnum.Green, "Mr. Green", null);
        player5ViewModel = new PlayerMenuItemViewModel(StandardEnums.PlayerEnum.Peacock, "Mrs. Peacock", null);
        player6ViewModel = new PlayerMenuItemViewModel(StandardEnums.PlayerEnum.Plum, "Professor Plum", null);

        player1View.BindViewModel(player1ViewModel);
        player2View.BindViewModel(player2ViewModel);
        player3View.BindViewModel(player3ViewModel);
        player4View.BindViewModel(player4ViewModel);
        player5View.BindViewModel(player5ViewModel);
        player6View.BindViewModel(player6ViewModel);

    }

    // Update is called once per frame
    void Update () {
        
	}
}
