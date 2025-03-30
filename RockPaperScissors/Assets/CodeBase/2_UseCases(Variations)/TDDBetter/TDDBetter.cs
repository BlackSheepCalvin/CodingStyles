using static OutCome;

public class TDDBetter : Variation
{
    private TDDBetterMatch gameMatch;

    public TDDBetter(IPrinter printer) : base(printer)
    {
        gameMatch = new TDDBetterMatch(printer);
    }
    public override void Start()
    {
        Print("Welcome to rock paper scissors!");
        Print("The rules are:");
        Print("Rock smashes scissors");
        Print("Scissors cuts paper");
        Print("Paper covers rock");
        gameMatch.AnnounceNextRound();
    }

    public override void DidPressKey(string key)
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
                Print("You won the match! Congrats!");
                gameMatch.AnnounceNextRound();
                break;
            case computerWin:
                Print("Computer wins! Better luck next time!");
                gameMatch.AnnounceNextRound();
                break;
            case tie:
                Print($"Aaand its a tie... somehow!");
                gameMatch.AnnounceNextRound();
                break;
            case inProgress:
                gameMatch.AnnounceNextRound();
                break;
        }
    }
}
