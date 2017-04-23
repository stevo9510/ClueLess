using System.Collections.Generic;
using UnityEngine;

public class StartUpGameManager : MonoBehaviour {

    [Tooltip(@"Add all the GamePlay objects that start in the initial camera frustrum to the list.  

They will be inactive at first until user connects to game.")]
    public List<GameObject> clueLessGraphicalGameplayObjects;

    private void Awake()
    {
        // deactivate all the game play objects at initial startup
        clueLessGraphicalGameplayObjects.ForEach(go => go.SetActive(false));
    }

    void Start ()
    {
        // Show the Connect to Game Panel when the game first starts
        // When the panel is closed ConnectToGame() is called
        ModalConnectToGamePanel.Instance().ShowPanel(() => ConnectToGame());
	}
	
    /// <summary>
    /// Close the Panel when the user tries to connect the game, and deactivate each object
    /// </summary>
    private void ConnectToGame()
    {
        clueLessGraphicalGameplayObjects.ForEach(go => go.SetActive(true));
        ModalConnectToGamePanel.Instance().ClosePanel();

        Network.Instance.ConnectToGame();
    }

}
