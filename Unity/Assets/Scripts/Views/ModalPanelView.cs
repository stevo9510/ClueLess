using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalPanelView : MonoBehaviour {

    public Button findGameButton;
    private static ModalPanelView modalPanel;
    public GameObject modalPanelObject;

    public static ModalPanelView Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanelView)) as ModalPanelView;
            if (!modalPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return modalPanel;
    }

    public void ShowPanel(UnityAction buttonHandler)
    {
        modalPanelObject.SetActive(true);
        findGameButton.onClick.RemoveAllListeners();
        findGameButton.onClick.AddListener(buttonHandler);
    }

    public void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }

}
