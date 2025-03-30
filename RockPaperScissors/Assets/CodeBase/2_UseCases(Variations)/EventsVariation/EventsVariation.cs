using static OutCome;
using static RPSEvent;

public class EventsVariation: Variation
{
    private GameRound gameRound;
    private FirstToNWinsCounter counter;
    private int badKeyCounter;
    private RPSEventListener listener;

    public EventsVariation(IPrinter printer) : base(printer) {
        listener = new RPSEventListener(printer);
        gameRound = new GameRound(listener);
        counter = new FirstToNWinsCounter(5);
    }
    
    public override void Start()
    {
        listener.FireEvent(GameStarts, null);
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
                listener.FireEvent(OnInvalidKey); 
            }
        }
    }

    private void EvaluateGameState(OutCome currentGameState)
    {
        switch (currentGameState)
        {
            case playerWin:
                listener.FireEvent(PlayerWinsMatch, null, counter.ComputerScore, counter.PlayerScore);
                NextGameStarts();
                break;
            case computerWin:
                listener.FireEvent(ComputerWinsMatch, null, counter.ComputerScore, counter.PlayerScore);
                NextGameStarts();
                break;
            case tie:
                listener.FireEvent(InProggressMatch, null, counter.ComputerScore, counter.PlayerScore);
                NextGameStarts();
                break;
            case inProgress:
                listener.FireEvent(InProggressMatch, null, counter.ComputerScore, counter.PlayerScore);
                listener.FireEvent(NextRoundAnnouncement);
                break;
        }
    }

    private void NextGameStarts()
    {
        counter.ResetScores();
        listener.FireEvent(NextMatchAnnouncement);
        listener.FireEvent(NextRoundAnnouncement);
    }
}
