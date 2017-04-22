using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerMenuPresenter : MonoBehaviour {
    public PlayerMenuItemView player1View;
    public PlayerMenuItemView player2View;
    public PlayerMenuItemView player3View;
    public PlayerMenuItemView player4View;
    public PlayerMenuItemView player5View;
    public PlayerMenuItemView player6View;

    private List<PlayerMenuItemViewModel> playerViewModels;

    private IServerToClientMessagePublisher messagePublisher;


    // Use this for initialization
    void Start ()
    {
        messagePublisher = Network.Instance;
        messagePublisher.EventAccusationMoveMade += MessagePublisher_EventAccusationMoveMade;
        messagePublisher.EventPlayerTurnChanged += MessagePublisher_EventPlayerTurnChanged;
        messagePublisher.EventPlayerAssignedID += MessagePublisher_EventPlayerAssignedID;
        messagePublisher.EventPlayersInGameChanged += MessagePublisher_EventPlayersInGameChanged;

        // Create all the ViewModels 
        PlayerMenuItemViewModel player1ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Scarlet);
        PlayerMenuItemViewModel player2ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Mustard);
        PlayerMenuItemViewModel player3ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Orchid);
        PlayerMenuItemViewModel player4ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Green);
        PlayerMenuItemViewModel player5ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Peacock);
        PlayerMenuItemViewModel player6ViewModel = CreateDefaultViewModel(StandardEnums.PlayerEnum.Plum);

        playerViewModels = new List<PlayerMenuItemViewModel>() { player1ViewModel, player2ViewModel,
            player3ViewModel, player4ViewModel, player5ViewModel, player6ViewModel};

        player1View.BindViewModel(player1ViewModel);
        player2View.BindViewModel(player2ViewModel);
        player3View.BindViewModel(player3ViewModel);
        player4View.BindViewModel(player4ViewModel);
        player5View.BindViewModel(player5ViewModel);
        player6View.BindViewModel(player6ViewModel);

        StandardValueRepository.Instance.PlayerDetailsChanged += Instance_PlayerDetailsChanged;
    }

    private void MessagePublisher_EventPlayersInGameChanged(PlayersInGameMessage msg)
    {
        playerViewModels.ForEach(player => 
            player.IsActive = msg.playerIDs.Contains(player.PlayerID));
    }

    private void MessagePublisher_EventPlayerAssignedID(PlayerAssignedIDMessage msg)
    {
        playerViewModels.Where(player => player.PlayerID == msg.playerID).First().IsClient = true;
    }

    private void MessagePublisher_EventPlayerTurnChanged(PlayerTurnMessage msg)
    {
        playerViewModels.ForEach(player => 
            player.IsAtTurn = (player.PlayerID == msg.playerID));
    }

    private void MessagePublisher_EventAccusationMoveMade(AccusationMoveMadeMessage msg)
    {
        var accusingPlayerViewModel = playerViewModels.Where(player => player.PlayerID == msg.playerID).First();
        accusingPlayerViewModel.IsEliminated = !msg.isCorrect;
        accusingPlayerViewModel.IsWinner = msg.isCorrect;
    }

    private void Instance_PlayerDetailsChanged(object sender, System.EventArgs e)
    {

    }

    private PlayerMenuItemViewModel CreateDefaultViewModel(StandardEnums.PlayerEnum playerID)
    {
        var viewModel = new PlayerMenuItemViewModel(playerID, StandardValueRepository.Instance.GetPlayerName(playerID));
        return viewModel;
    }

}
