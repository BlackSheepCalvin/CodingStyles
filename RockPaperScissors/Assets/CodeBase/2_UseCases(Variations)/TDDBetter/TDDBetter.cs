using static OutCome;
using static RockPaperScissorsConsts;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TDDBetter : Variation
{
    private TDDBetterMatch gameMatch;
    private int badKeyCounter;

    public TDDBetter(IPrinter printer) : base(printer)
    {
        gameMatch = new TDDBetterMatch(printer);
    }
    public override void Start()
    {
        Rules.ForEach(x =>
        {
            Print(x);
        });
    }

    public override void DidPressKey(string key)
    {
        var sign = key.decodeSign();
        if (sign.HasValue)
        {
            badKeyCounter = 0;
            EvaluateGameState(gameMatch.EvaluatePlayerSign(sign.Value));
        }
        else
        {
            badKeyCounter++;
            if (badKeyCounter == 3)
            {
                Print(OnInvalidKey);
            }
        }
    }

    private void EvaluateGameState(OutCome outCome)
    {
        switch (outCome)
        {
            case playerWin:
                Print(PlayerWinsMatch);
                Print(NextMatchAnnouncement);
                Print(NextRoundAnnouncement);
                break;
            case computerWin:
                Print(ComputerWinsMatch);
                Print(NextMatchAnnouncement);
                Print(NextRoundAnnouncement);
                break;
            case tie:
                Print($"Aaand its a tie... somehow!");
                Print(NextMatchAnnouncement);
                Print(NextRoundAnnouncement); ;
                break;
            case inProgress:
                Print(NextRoundAnnouncement);
                break;
        }
    }
}
