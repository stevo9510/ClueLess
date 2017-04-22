using System.ComponentModel;
using UnityEngine.UI;

public class PlayerMenuItemViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private StandardEnums.PlayerEnum _playerID;
    public StandardEnums.PlayerEnum PlayerID
    {
        get { return _playerID; }
    }

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
            if (_playerName != value)
            {
                _playerName = value;
                OnPropertyChanged("PlayerName");
            }
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

    private bool _isActive;
    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            if (_isActive != value)
            {
                _isActive = value;
                OnPropertyChanged("IsActive");
            }
        }
    }

    private bool _isEliminated;
    public bool IsEliminated
    {
        get { return _isEliminated; }
        set
        {
            if (_isEliminated != value)
            {
                _isEliminated = value;
                OnPropertyChanged("IsEliminated");
            }
        }
    }

    private bool _isAtTurn;
    public bool IsAtTurn
    {
        get { return _isAtTurn; }
        set
        {
            if (_isAtTurn != value)
            {
                _isAtTurn = value;
                OnPropertyChanged("IsAtTurn");
            }
        }
    }

    private bool _isClient;

    public bool IsClient
    {
        get { return _isClient; }
        set
        {
            if (_isClient != value)
            {
                _isClient = value;
                OnPropertyChanged("IsClient");
            }
        }
    }

    private bool _isWinner;

    public bool IsWinner
    {
        get { return _isWinner; }
        set
        {
            if (value != _isWinner)
            {
                _isWinner = value;
                OnPropertyChanged("IsWinner");
            }
        }
    }

    #endregion

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="playerName"></param>
    /// <param name="playerPortrait"></param>
    public PlayerMenuItemViewModel(StandardEnums.PlayerEnum playerID, string playerName)
    {
        _playerID = playerID;
        _playerName = playerName;
        _playerNote = string.Empty;
        _isActive = false;
        _isEliminated = false;
        _isAtTurn = false;
        _isClient = false;
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
