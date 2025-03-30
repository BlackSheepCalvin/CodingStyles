using static OutCome;

public class DDPGameMatch
{
    public OutCome OutCome;
    private DDPGameRound gameRound;
    private GameData data;
    public int PlayerScore { get; private set; }
    public int ComputerScore { get; private set; }
    public DDPGameMatch(IPrinter printer)
    {
        // Hint: Fearless developer: I wonder what happens if data does not exist yet when i start using this class...
        // It is a struct, but the struct contains lists. I am not very good at C#.
        // I don't know how to handle best this scenario yet. But, maybe i dont have to know! Because maybe the problem doesn't even exist.
        // So why should i waste my time? And why should i come up with some mediocre solution where i try to make the code still work somehow?!
        // A mediocre solution would just make debugging more difficult. And also the extra code would make readability worse.
        // I want my application to crash if something is not right. And find the problem immediately. And decide how to handle this perfectly. I am fearless. :D
        // If crashing is not ok, i can create a tech debt, and investigate how should we handle this edge case.
        // I dont want my application to become some kind of zombie, that kindof works and never crashes, but sometimes it works... interestingly...
        // I want my application to die if it is not worthy. So it can reborn stronger, and i can drain knowledge from that! :D
        gameRound = new DDPGameRound(printer, data);
    }

    public void SetData(GameData data)
    {
        this.data = data;
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
