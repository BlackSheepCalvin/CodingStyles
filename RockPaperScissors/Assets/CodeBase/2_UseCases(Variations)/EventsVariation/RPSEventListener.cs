using static RPSEvent;
using static Sign;
using static RockPaperScissorsConsts;

public enum RPSEvent
{ // Hint: Code Smells. We created a new enum. To me enums are an instant code smell (sometimes they are fine but they are always sus).
  // Especially if we compare this solution to other solutions where this enum does not need to exist.
    PlayerShowsSign,
    ComputerShowsSign,
    PlayerWinsMatch,
    ComputerWinsMatch,
    InProggressMatch,
    GameStarts,
    OnInvalidKey,
    NextRoundAnnouncement,
    NextMatchAnnouncement
}

public class RPSEventListener : PrinterUser // Hint: Simplicity + YAGNI: RPSEventListener is not really an observer pattern, we dont subscribe to anything,
                                            // RPSEventListener.FireEvent will be directly called, and RPSEventListener will be passed everywere.
                                            // Thats ok. We dont need to overcomplicate this yet. This works. And it is simple. We can refactor anytime we need to.
{
    public RPSEventListener(IPrinter printer): base (printer) { }
    private Sign lastPlayerSign;

    public void FireEvent(RPSEvent newEvent, Sign? sign = null, int computerScore = 0, int playerScore = 0) // Lol now i'm adding Signs here too... and computerScore+playerScore. Totally decoupled...
    {
        switch (newEvent) // Hint: Code Smell / False Decoupling. This is an other "code smell". A new switch that we shouldn't need. An other complexity.
        {                 // But... its nice that everything is together here. We can see in one place how we react to different events.
                          // An other question is that we have no idea what these events actually are, and when they actually happen... :D
                          // Some people would say "but its a good thing!" "Its decoupled!". Low coupling, high cohesion, right?!
                          // Sorry i'm gonna complain a lot. I don't like observer pattern.
            case GameStarts:
                Rules.ForEach(rule =>
                {
                    Print(rule);
                });
                break;
            case RPSEvent.OnInvalidKey:
                Print(RockPaperScissorsConsts.OnInvalidKey);
                break;
            case PlayerShowsSign:
                lastPlayerSign = sign.Value;
                break;
            case ComputerShowsSign:
                EvaluateSigns(lastPlayerSign, sign.Value);
                break;
            case RPSEvent.PlayerWinsMatch:
                Print($"Computer: {computerScore}, Player: {playerScore}");
                Print(RockPaperScissorsConsts.PlayerWinsMatch);
                break;
            case RPSEvent.ComputerWinsMatch:
                Print($"Computer: {computerScore}, Player: {playerScore}");
                Print(RockPaperScissorsConsts.ComputerWinsMatch);
                break;
            case InProggressMatch:
                Print($"Computer: {computerScore}, Player: {playerScore}");
                break;
            case RPSEvent.NextRoundAnnouncement:
                Print(RockPaperScissorsConsts.NextRoundAnnouncement);
                break;
            case RPSEvent.NextMatchAnnouncement:
                Print(RockPaperScissorsConsts.NextMatchAnnouncement);
                break;
        }

        // Hint: Hacks, Hiding mistakes: Strawman: "Ok, if switch is a code smell, how about this:"
        /*
        private Dictionary<RPSEvent, Action<IPrinter>> eventActions = new Dictionary<RPSEvent, Action<IPrinter>>
        {
            { RPSEvent.playerWinsMatch, x => x.Print("congratulations!") },
            { RPSEvent.computerWinsMatch, x => x.Print("better luck next time!") }
        };
        //And then you can do:
        eventActions[newEvent].Invoke(printer); //AND thats it!!! No need to use switch!!! ONE LINE OF CODE! Beautiful!

        ...
        Congratulations, you just used a hack as perfume on sh*t, so it is now harder to find.
        */
    }

    private void EvaluateSigns(Sign playerSign, Sign computerSign) // Deja vu...
    {
        var prefix = $"You showed {playerSign}! Computer showed {computerSign}! -";
        if (playerSign == rock)
        {
            if (computerSign == rock)
            {
                Print($"{prefix} {Tie}!");
            }
            else if (computerSign == paper)
            {
                Print($"{prefix} {PaperWin}!");
            }
            else if (computerSign == scissors)
            {
                Print($"{prefix} {RockWin}!");
            }
        }
        else if (playerSign == paper)
        {
            if (computerSign == rock)
            {
                Print($"{prefix} {PaperWin}!");
            }
            else if (computerSign == paper)
            {
                Print($"{prefix} {Tie}!");
            }
            else if (computerSign == scissors)
            {
                Print($"{prefix} {ScissorsWin}!");
            }
        }
        else if (playerSign == scissors)
        {
            if (computerSign == rock)
            {
                Print($"{prefix} {RockWin}!");
            }
            else if (computerSign == paper)
            {
                Print($"{prefix} {ScissorsWin}!");
            }
            else if (computerSign == scissors)
            {
                Print($"{prefix} {Tie}!");
            }
        }
    }
}
