using static RockPaperScissorsConsts;
using static Sign;
using static OutCome;

public class GameRound: PrinterUser
{
    private IRandom random = ServiceProvider.Random;
    public OutCome LastOutCome { get; private set; }
    public GameRound(IPrinter printer): base (printer) { }

    public void EvaluatePlayerSign(Sign playerSign)
    {
        var computerSign = random.RandomSign();
        var prefix = $"You showed {playerSign}! Computer showed {computerSign}! -";
        if (playerSign == rock)
        {
            if (computerSign == rock)
            {
                DidTie($"{prefix} {Tie}!");
            }
            else if (computerSign == paper)
            {
                DidComputerWin($"{prefix} {PaperWin}!");
            }
            else if (computerSign == scissors)
            {
                DidPlayerWin($"{prefix} {RockWin}!");
            }
        }
        else if (playerSign == paper)
        {
            if (computerSign == rock)
            {
                DidPlayerWin($"{prefix} {PaperWin}!");
            }
            else if (computerSign == paper)
            {
                DidTie($"{prefix} {Tie}!");
            }
            else if (computerSign == scissors)
            {
                DidComputerWin($"{prefix} {ScissorsWin}!");
            }
        }
        else if (playerSign == scissors)
        {
            if (computerSign == rock)
            {
                DidComputerWin($"{prefix} {RockWin}!");
            }
            else if (computerSign == paper)
            {
                DidPlayerWin($"{prefix} {ScissorsWin}!");
            }
            else if (computerSign == scissors)
            {
                DidTie($"{prefix} {Tie}!");
            }
        }
    }

    void DidTie(string text) // Hint: I could do a side effect for LastOutCome setter, instead of doing this, but it would be stupid imho.
    {
        Print(text);
        LastOutCome = tie;
    }
    void DidPlayerWin(string text)
    {
        Print(text);
        LastOutCome = playerWin;
    }
    void DidComputerWin(string text)
    {
        Print(text);
        LastOutCome = computerWin;
    }
}
