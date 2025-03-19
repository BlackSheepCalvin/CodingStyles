using static RockPaperScissorsConsts;
using static Sign;
using static OutCome;
using System;

public class SimplestOne: Variation, KeyInputUser
{
    private GameRound gameRound;
    private FirstToNWinsCounter counter;

    public SimplestOne(IPrinter printer) : base(printer) {
        gameRound = new GameRound(printer);
        counter = new FirstToNWinsCounter(5);
    }
    
    public override void Start()
    {
        printer.Print("Hey these are the rules, You press key R for rock, P for paper, S for scissors. The computer also generates a sign, and then we determine the winner based on the rules!");
        printer.Print("The rules are:");
        printer.Print(RockWin);
        printer.Print(PaperWin);
        printer.Print(ScissorsWin);
        printer.Print($"Whoever first gets {counter.TargetScore} wins, wins the match!");
        NextGameStarts();
    }

    public void DidPressKey(string key)
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
