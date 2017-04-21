using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// This is a singleton for the Modal Connect to Game Panel that is shown at the start of the game
/// </summary>
public class ModalConnectToGamePanel : MonoBehaviour {

    private static ModalConnectToGamePanel modalPanel;

    public static ModalConnectToGamePanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalConnectToGamePanel)) as ModalConnectToGamePanel;
            if (!modalPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return modalPanel;
    }

    [Tooltip("The Find Game... button that can be clicked from the connection panel")]
    public Button findGameButton;
    [Tooltip("The actual game object / panel that should be displayed as a modal")]
    public GameObject modalPanelObject;

    /// <summary>
    /// When panel is shown, make active and wire up button handler callback
    /// </summary>
    /// <param name="buttonHandler"></param>
    public void ShowPanel(UnityAction buttonHandler)
    {
        modalPanelObject.SetActive(true);
        findGameButton.onClick.RemoveAllListeners();
        findGameButton.onClick.AddListener(buttonHandler);
    }

    /// <summary>
    /// Set inactive
    /// </summary>
    public void ClosePanel()
    {
        findGameButton.onClick.RemoveAllListeners();
        modalPanelObject.SetActive(false);
    }

}
