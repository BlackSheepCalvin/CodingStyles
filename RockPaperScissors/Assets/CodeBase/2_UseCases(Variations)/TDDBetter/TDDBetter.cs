using static OutCome;

public class TDDBetter : Variation, KeyInputUser
{
    private TDDBetterMatch gameMatch;

    public TDDBetter(IPrinter printer) : base(printer)
    {
        gameMatch = new TDDBetterMatch(printer);
    }
    public override void Start()
    {
        printer.Print("Welcome to rock paper scissors!");
        printer.Print("The rules are:");
        printer.Print("Rock smashes scissors");
        printer.Print("Scissors cuts paper");
        printer.Print("Paper covers rock");
        gameMatch.AnnounceNextRound();
    }

    public void DidPressKey(string key)
    {
        var sign = key.decodeSign();
        if (sign.HasValue)
        {
            EvaluateGameState(gameMatch.EvaluatePlayerSign(sign.Value));
        }
    }

    private void EvaluateGameState(OutCome outCome)
    {
        switch (outCome)
        {
            case playerWin:
                printer.Print("You won the match! Congrats!");
                gameMatch.AnnounceNextRound();
                break;
            case computerWin:
                printer.Print("Computer wins! Better luck next time!");
                gameMatch.AnnounceNextRound();
                break;
            case tie:
                printer.Print($"Aaand its a tie... somehow!");
                gameMatch.AnnounceNextRound();
                break;
            case inProgress:
                gameMatch.AnnounceNextRound();
                break;
        }
    }
}
