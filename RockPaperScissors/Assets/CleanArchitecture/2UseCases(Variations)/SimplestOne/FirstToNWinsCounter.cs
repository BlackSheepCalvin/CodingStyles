using static OutCome;

public class FirstToNWinsCounter
{
    public int TargetScore { get; private set; }
    public int PlayerScore { get; private set; }
    public int ComputerScore { get; private set; }
    public FirstToNWinsCounter(int targetScore)
    {
        TargetScore = targetScore;
    }

    public void ResetScores()
    {
        PlayerScore = 0; 
        ComputerScore = 0;
    }

    public void ProcessOutcome(OutCome outCome)
    {
        if (outCome == playerWin) {
            PlayerScore ++;
        } else if (outCome == computerWin) {
            ComputerScore ++;
        }
    }

    public OutCome CurrentGameState()
    {
        if (PlayerScore >= TargetScore && ComputerScore >= TargetScore)
        {
            return tie;
        }

        if (PlayerScore >= TargetScore)
        {
            return playerWin;
        }
        
        if (ComputerScore >= TargetScore)
        {
            return computerWin;
        }

        return inProgress;
    }
}
