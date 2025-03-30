using static OutCome;

public class TDD : Variation // Hint: TDD = Test Driven Development
{
    private ITDDGameMatch gameMatch;

    public TDD(IPrinter printer, ITDDGameMatch gameMatch) : base(printer)
    { // Used by Unit tests
        this.gameMatch = gameMatch;
    }

    public TDD(IPrinter printer) : base(printer)
    { // Used by Initializer (Production code)
        gameMatch = new TDDGameMatch(printer, new TDDGameRound(printer), 5);
    }
    public override void Start()
    {
        Print("Welcome to rock paper scissors!");
        Print("The rules are:");
        Print("Rock smashes scissors");
        Print("Scissors cuts paper");
        Print("Paper covers rock");
        Print("");
        gameMatch.AnnounceNextRound();
    }

    public override void DidPressKey(string key)
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
                Print("You won the match! Congrats!");
                gameMatch.AnnounceNextRound();
                break;
            case computerWin:
                Print("Better luck next time! Computer won the match!");
                gameMatch.AnnounceNextRound();
                break;
            case tie:
                Print($"Aaand its a tie... somehow!");
                gameMatch.AnnounceNextRound();
                break;
            case inProgress:
                Print($"score: {gameMatch.PlayerScore} - {gameMatch.ComputerScore}");
                gameMatch.AnnounceNextRound();
                break;
        }
    }
}
