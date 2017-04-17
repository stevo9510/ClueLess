using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public List<GameObject> clueLessGameObjects;

    private void Awake()
    {
        clueLessGameObjects.ForEach(go => go.SetActive(false));
    }

    // Use this for initialization
    void Start () {
        UnityAction ua = new UnityAction(ConnectToGame);
        ModalPanelView.Instance().ShowPanel(ua);
	}
	
    private void ConnectToGame()
    {
        ModalPanelView.Instance().ClosePanel();
        clueLessGameObjects.ForEach(go => go.SetActive(true));
    }


    // Update is called once per frame
    void Update () {
		
	}
}
