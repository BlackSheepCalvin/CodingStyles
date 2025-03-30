using System;
using static RockPaperScissorsConsts;
using static Sign;
using static OutCome;

public interface ITDDGameRound
{
    void EvaluatePlayerSign(Sign playerSign);

    OutCome LastOutCome { get; }
}

public class TDDGameRound: PrinterUser, ITDDGameRound
{
    private IRandom random;
    public OutCome LastOutCome { get; private set; }
    public TDDGameRound(IPrinter printer, IRandom random = null): base(printer)
    {
        this.random = random ?? ServiceProvider.Random;
    }

    public void EvaluatePlayerSign(Sign playerSign)
    {
        var computerSign = random.RandomSign();
        Print($"Player: {playerSign}");
        Print($"Computer: {computerSign}");
        if (playerSign == rock)
        {
            if (computerSign == rock)
            {
                DidTie();
            }
            else if (computerSign == paper)
            {
                Print(PaperWin);
                DidComputerWin();
            }
            else if (computerSign == scissors)
            {
                Print(RockWin);
                DidPlayerWin();
            }
        }
        else if (playerSign == paper)
        {
            if (computerSign == rock)
            {
                Print(PaperWin);
                DidPlayerWin();
            }
            else if (computerSign == paper)
            {
                DidTie();
            }
            else if (computerSign == scissors)
            {
                Print(ScissorsWin);
                DidComputerWin();
            }
        }
        else if (playerSign == scissors)
        {
            if (computerSign == rock)
            {
                Print(RockWin);
                DidComputerWin();
            }
            else if (computerSign == paper)
            {
                Print(ScissorsWin);
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
        Print(Tie);
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
