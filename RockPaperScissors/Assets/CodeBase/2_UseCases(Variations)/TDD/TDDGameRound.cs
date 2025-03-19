using System;
using static RockPaperScissorsConsts;
using static Sign;
using static OutCome;

public interface ITDDGameRound
{
    void EvaluatePlayerSign(Sign playerSign);

    OutCome LastOutCome { get; }
}

public class TDDGameRound: ITDDGameRound
{
    private IPrinter printer;
    private IRandom random;
    public OutCome LastOutCome { get; private set; }
    public TDDGameRound(IPrinter printer, IRandom random = null)
    {
        this.printer = printer;
        this.random = random ?? ServiceProvider.Random;
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

    void DidTie()
    {
        printer.Print(Tie);
        LastOutCome = tie;
    }
    void DidPlayerWin()
    {
        printer.Print(PlayerWin);
        LastOutCome = playerWin;
    }
    void DidComputerWin()
    {
        printer.Print(ComputerWin);
        LastOutCome = computerWin;
    }
}
