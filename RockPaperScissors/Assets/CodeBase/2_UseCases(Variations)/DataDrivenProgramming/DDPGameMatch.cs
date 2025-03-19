using static OutCome;

public class DDPGameMatch
{
    public OutCome OutCome;
    private DDPGameRound gameRound;
    private GameData data;
    public int PlayerScore { get; private set; }
    public int ComputerScore { get; private set; }
    public DDPGameMatch(IPrinter printer, GameData data)
    {
        this.data = data;
        gameRound = new DDPGameRound(printer, data);
    }
    public void EvaluatePlayerSign(string signId)
    {
        gameRound.EvaluatePlayerSign(signId);
        switch (gameRound.OutCome) {
            case playerWin:
                PlayerScore++;
                break;
            case computerWin:
                ComputerScore++;
                break;
            case tie:
                break;
        }
        UpdateGameState();
    }

    void UpdateGameState()
    {
        if (PlayerScore >= data.matchLength && ComputerScore >= data.matchLength)
        {
            OutCome = tie;
            Reset();
        }

        if (PlayerScore >= data.matchLength)
        {
            OutCome = playerWin;
            Reset();
        }

        if (ComputerScore >= data.matchLength)
        {
            OutCome = computerWin;
            Reset();
        }

        OutCome = inProgress;
    }

    void Reset()
    {
        PlayerScore = 0;
        ComputerScore = 0;
    }
}
