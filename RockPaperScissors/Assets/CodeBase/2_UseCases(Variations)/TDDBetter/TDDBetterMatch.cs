using static OutCome;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

class TDDBetterMatch: PrinterUser
{
    internal int PlayerScore { get; private set; }
    internal int ComputerScore { get; private set; }

    private TDDBetterRound gameRound;
    
    internal TDDBetterMatch(IPrinter printer): base(printer)
    {
        gameRound = new TDDBetterRound(printer);
    }

    internal OutCome EvaluatePlayerSign(Sign playerSign)
    {
        var roundOutCome = gameRound.EvaluatePlayerSign(playerSign);
        switch (roundOutCome)
        {
            case playerWin:
                ++PlayerScore; // Hint: Readability: i want ++PlayerScore here, and not hidden into the next line...
                Print($"Computer: {ComputerScore}, Player: {PlayerScore}"); // Print($"Computer: {ComputerScore}, Player: {++PlayerScore}"); - this would be great if you wanted to confuse people reading your code :D
                break;
            case computerWin:
                ++ComputerScore;
                Print($"Computer: {ComputerScore}, Player: {PlayerScore}");
                break;
            case tie:
                Print($"Computer: {ComputerScore}, Player: {PlayerScore}");
                break;
        }

        if (PlayerScore >= 5)
        {
            PlayerScore = 0;
            ComputerScore = 0;
            return playerWin;
        }
        else if (ComputerScore >= 5)
        {
            PlayerScore = 0;
            ComputerScore = 0;
            return computerWin;
        }
        else
        {
            return inProgress;
        }
    }
}
