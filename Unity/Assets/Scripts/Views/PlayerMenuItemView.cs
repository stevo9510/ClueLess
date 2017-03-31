using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuItemView : MonoBehaviour
{
    public Image PlayerPortrait;
    public Text PlayerName;
    public Text PlayerNote;

    public void BindViewModel(PlayerMenuItemViewModel viewModel)
    {
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        //switch(e.PropertyName)
        //{
        //    case 
        //}
    }
}
