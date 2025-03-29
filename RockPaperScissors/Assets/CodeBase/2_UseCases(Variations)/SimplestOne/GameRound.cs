using System;
using static RockPaperScissorsConsts;
using static Sign;
using static OutCome;

public class GameRound
{
    private IPrinter printer;
    private IRandom random = ServiceProvider.Random;
    public OutCome LastOutCome { get; private set; }
    public GameRound(IPrinter printer) { 
        this.printer = printer;
    }
    public void AnnounceNextRound()
    {
        printer.Print("");
        printer.Print("3...2..1..");
    }

    public void EvaluatePlayerSign(Sign playerSign)
    {
        var computerSign = random.RandomSign();
        printer.Print($"Player: {playerSign}");
        printer.Print($"Computer: {computerSign}");
        if (playerSign == rock)
        {
            if (computerSign == rock)
            {
                DidTie();
            }
            else if (computerSign == paper)
            {
                printer.Print(PaperWin);
                DidComputerWin();
            }
            else if (computerSign == scissors)
            {
                printer.Print(RockWin);
                DidPlayerWin();
            }
        }
        else if (playerSign == paper)
        {
            if (computerSign == rock)
            {
                printer.Print(PaperWin);
                DidPlayerWin();
            }
            else if (computerSign == paper)
            {
                DidTie();
            }
            else if (computerSign == scissors)
            {
                printer.Print(ScissorsWin);
                DidComputerWin();
            }
        }
        else if (playerSign == scissors)
        {
            if (computerSign == rock)
            {
                printer.Print(RockWin);
                DidComputerWin();
            }
            else if (computerSign == paper)
            {
                printer.Print(ScissorsWin);
                DidPlayerWin();
            }
            else if (computerSign == scissors)
            {
                DidTie();
            }
        }
    }

    void DidTie() // Hint: I could do a side effect for LastOutCome setter, instead of doing this, but it would be stupid imho.
    {
        printer.Print(Tie);
        LastOutCome = tie;
    }
    void DidPlayerWin()
    {
        LastOutCome = playerWin;
    }
    void DidComputerWin()
    {
        LastOutCome = computerWin;
    }
}
