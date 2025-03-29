using static OutCome;

public class SimplestOne: Variation
{
    private GameRound gameRound;
    private FirstToNWinsCounter counter;

    public SimplestOne(IPrinter printer) : base(printer) {
        gameRound = new GameRound(printer);
        counter = new FirstToNWinsCounter(5);
    }
    
    public override void Start()
    {
        printer.Print("Welcome to rock paper scissors!");
        printer.Print("The rules are:");
        printer.Print("Rock smashes scissors");
        printer.Print("Scissors cuts paper");
        printer.Print("Paper covers rock");
        NextGameStarts();
    }

    public override void DidPressKey(string key)
    {
        var sign = key.decodeSign();
        if (sign.HasValue)
        {
            gameRound.EvaluatePlayerSign(sign.Value);
            counter.ProcessOutcome(gameRound.LastOutCome);
            EvaluateGameState(counter.CurrentGameState());
        }
    }

    private void EvaluateGameState(OutCome currentGameState)
    {
        switch (currentGameState)
        {
            case playerWin:
                printer.Print("Congratulations! You won the match!");
                NextGameStarts();
                break;
            case computerWin:
                printer.Print("Better luck next time! Computer won the match!");
                NextGameStarts();
                break;
            case tie:
                printer.Print($"Aaand you both reached {counter.TargetScore} wins... somehow!");
                NextGameStarts();
                break;
            case inProgress:
                printer.Print($"Computer: {counter.ComputerScore}");
                printer.Print($"Player: {counter.PlayerScore}");
                gameRound.AnnounceNextRound();
                break;
        }
    }

    private void NextGameStarts()
    {
        counter.ResetScores();
        gameRound.AnnounceNextRound();
    }
}
