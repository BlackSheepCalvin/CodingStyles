using static OutCome;

public class TestDrivenDevelopment : Variation, KeyInputUser
{
    private ITDDGameMatch gameMatch;

    public TestDrivenDevelopment(IPrinter printer, ITDDGameMatch gameMatch) : base(printer)
    { // Used by Unit tests
        this.gameMatch = gameMatch;
    }

    public TestDrivenDevelopment(IPrinter printer) : base(printer)
    { // Used by Initializer (Production code)
        gameMatch = new TDDGameMatch(printer, new TDDGameRound(printer), 5);
    }
    public override void Start()
    {
        printer.Print("Welcome to rock paper scissors!");
        printer.Print("The rules are:");
        printer.Print("Rock smashes scissors");
        printer.Print("Scissors cuts paper");
        printer.Print("Paper covers rock");
        printer.Print("");
        gameMatch.AnnounceNextRound();
    }

    public void DidPressKey(string key)
    {
        var sign = key.decodeSign();
        if (sign.HasValue)
        {
            gameMatch.EvaluatePlayerSign(sign.Value);
            EvaluateGameState();
        }
    }

    private void EvaluateGameState()
    {
        switch (gameMatch.OutCome)
        {
            case playerWin:
                printer.Print("You won the match! Congrats!");
                gameMatch.AnnounceNextRound();
                break;
            case computerWin:
                printer.Print("Better luck next time! Computer won the match!");
                gameMatch.AnnounceNextRound();
                break;
            case tie:
                printer.Print($"Aaand its a tie... somehow!");
                gameMatch.AnnounceNextRound();
                break;
            case inProgress:
                printer.Print($"score: {gameMatch.PlayerScore} - {gameMatch.ComputerScore}");
                gameMatch.AnnounceNextRound();
                break;
        }
    }
}
