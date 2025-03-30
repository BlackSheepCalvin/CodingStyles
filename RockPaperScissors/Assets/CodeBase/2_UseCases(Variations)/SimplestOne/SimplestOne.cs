using static OutCome;
using static RockPaperScissorsConsts;

public class SimplestOne: Variation
{
    private GameRound gameRound;
    private FirstToNWinsCounter counter;
    private int badKeyCounter;

    public SimplestOne(IPrinter printer) : base(printer) {
        gameRound = new GameRound(printer);
        counter = new FirstToNWinsCounter(5);
    }
    
    public override void Start()
    {
        Rules.ForEach(rule =>
        {
            Print(rule);
        });
    }

    public override void DidPressKey(string key)
    {
        var sign = key.decodeSign();
        if (sign.HasValue)
        {
            badKeyCounter = 0;
            gameRound.EvaluatePlayerSign(sign.Value);
            counter.ProcessOutcome(gameRound.LastOutCome);
            EvaluateGameState(counter.CurrentGameState());
        } else
        {
            badKeyCounter++;
            if (badKeyCounter == 3)
            {
                Print(OnInvalidKey);
            }
        }
    }

    private void EvaluateGameState(OutCome currentGameState)
    {
        switch (currentGameState)
        {
            case playerWin:
                Print($"Computer: {counter.ComputerScore}, Player: {counter.PlayerScore}");
                Print(PlayerWinsMatch);
                NextGameStarts();
                break;
            case computerWin:
                Print($"Computer: {counter.ComputerScore}, Player: {counter.PlayerScore}");
                Print(ComputerWinsMatch);
                NextGameStarts();
                break;
            case tie:
                Print($"Computer: {counter.ComputerScore}, Player: {counter.PlayerScore}");
                Print($"Aaand you both reached {counter.TargetScore} wins... somehow!");
                NextGameStarts();
                break;
            case inProgress:
                Print($"Computer: {counter.ComputerScore}, Player: {counter.PlayerScore}");
                Print(NextRoundAnnouncement);
                break;
        }
    }

    private void NextGameStarts()
    {
        counter.ResetScores();
        Print(NextMatchAnnouncement);
        Print(NextRoundAnnouncement);
    }
}
