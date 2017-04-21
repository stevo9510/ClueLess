using System;

[Serializable]
public class AccusationMoveMadeMessage : CardTypeTuple
{
    public StandardEnums.PlayerEnum playerThatMadeAccusation;
    public bool isCorrect;
}
