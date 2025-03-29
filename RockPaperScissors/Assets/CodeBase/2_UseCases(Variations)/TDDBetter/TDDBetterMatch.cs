using static OutCome;

class TDDBetterMatch
{
    internal int PlayerScore { get; private set; }
    internal int ComputerScore { get; private set; }

    private TDDBetterRound gameRound;
    private IPrinter printer;

    internal TDDBetterMatch(IPrinter printer)
    {
        this.printer = printer;
        this.gameRound = new TDDBetterRound(printer);
    }

    internal OutCome EvaluatePlayerSign(Sign playerSign)
    {
        var roundOutCome = gameRound.EvaluatePlayerSign(playerSign);
        switch (roundOutCome)
        {
            case playerWin:
                ++PlayerScore; // Hint: Readability: i want ++PlayerScore here, and not hidden into the next line...
                printer.Print($"Score: P: {PlayerScore}, C: {ComputerScore}"); // printer.Print($"Score: P: {++PlayerScore}, C: {ComputerScore}"); - this would be great if you wanted to confuse people reading your code :D
                break;
            case computerWin:
                ++ComputerScore;
                printer.Print($"Score: P: {PlayerScore}, C: {ComputerScore}");
                break;
            case tie:
                printer.Print($"Score: P: {PlayerScore}, C: {ComputerScore}");
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

    public void AnnounceNextRound()
    {
        printer.Print("");
        printer.Print("3...2..1..");
    }
}
