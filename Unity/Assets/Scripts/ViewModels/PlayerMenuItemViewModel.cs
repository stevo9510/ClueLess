using System.ComponentModel;
using UnityEngine.UI;

public class PlayerMenuItemViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    #region Getter/Setter Properties that fire OnNotifyPropertyChanged When Set

    private Image _playerPortrait;
    public Image PlayerPortrait
    {
        get { return _playerPortrait; }
        set
        {
            _playerPortrait = value;
            OnPropertyChanged("PlayerPortrait");
        }
    }

    private string _playerName;
    public string PlayerName
    {
        get { return _playerName; }
        set
        {
            _playerName = value;
            OnPropertyChanged("PlayerName");
        }
    }

    private string _playerNote;
    public string PlayerNote
    {
        get { return _playerNote; }
        set
        {
            _playerNote = value;
            OnPropertyChanged("PlayerNote");
        }
    }

    private StandardEnums.PlayerEnum _playerID;
    public StandardEnums.PlayerEnum PlayerID
    {
        get { return _playerID; }
    }

    private bool _isActive;
    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            _isActive = value;
            OnPropertyChanged("IsActive");
        }
    }

    private bool _isEliminated;
    public bool IsEliminated
    {
        get { return _isEliminated; }
        set
        {
            _isEliminated = value;
            OnPropertyChanged("IsEliminated");
        }
    }

    private bool _isAtTurn;
    public bool IsAtTurn
    {
        get { return _isAtTurn; }
        set
        {
            _isAtTurn = value;
            OnPropertyChanged("IsAtTurn");
        }
    }

    #endregion

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="playerName"></param>
    /// <param name="playerPortrait"></param>
    public PlayerMenuItemViewModel(StandardEnums.PlayerEnum playerID, string playerName, Image playerPortrait)
    {
        _playerID = playerID;
        _playerName = playerName;
        _playerNote = string.Empty;
        _playerPortrait = playerPortrait;
        _isActive = false;
        _isEliminated = false;
        _isAtTurn = false;

    }

    /// <summary>
    /// Fire OnPropertyChanged event
    /// </summary>
    /// <param name="propName"></param>
    protected void OnPropertyChanged(string propName)
    {
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propName));
    }
}
