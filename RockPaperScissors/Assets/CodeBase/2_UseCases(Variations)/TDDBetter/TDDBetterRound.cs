using static Sign;
using static OutCome;
using static RockPaperScissorsConsts;
class TDDBetterRound: PrinterUser
{
    IRandom random = ServiceProvider.Random; // Hint: Fearless developer: this is a new idea i am not sure if its ok to do this,
                                             // but until i encounter an issue, this is more convenient, and i am fearless.
                                             // This could cause issues if I want to test in parallel.
                                             // If it does, i can refactor, and learn in the process.
                                             // If it doesnt, i also learn + my code is easier to read
    //private IPrinter printer; // Hint: Overengineering: if i do the serviceProvider with random why dont i do the same with printer? -
                              // I am not sure what is the best yet, so why waste time refactoring everything?
                              // But - I can create a tech debt ticket for someone to investigate. Especially if ServiceProvider.Random turns out to be ok.
                              // Note: although this is now simplified, i leave these thoughts here still

    internal TDDBetterRound(IPrinter printer): base(printer) { }

    internal OutCome EvaluatePlayerSign(Sign playerSign)
    {
        var computerSign = random.RandomSign();
        var prefix = $"You showed {playerSign}! Computer showed {computerSign}! -";
        if (playerSign == rock)
        {
            if (computerSign == rock)
            {
                Print($"{prefix} {Tie}!");
                return tie;
            }
            else if (computerSign == paper)
            {
                Print($"{prefix} {PaperWin}!");
                return computerWin;
            }
            else if (computerSign == scissors)
            {
                Print($"{prefix} {RockWin}!");
                return playerWin;
            }
        }
        else if (playerSign == paper)
        {
            if (computerSign == rock)
            {
                Print($"{prefix} {PaperWin}!");
                return playerWin;
            }
            else if (computerSign == paper)
            {
                Print($"{prefix} {Tie}!");
                return tie;
            }
            else if (computerSign == scissors)
            {
                Print($"{prefix} {ScissorsWin}!");
                return computerWin;
            }
        }
        else if (playerSign == scissors)
        {
            if (computerSign == rock)
            {
                Print($"{prefix} {RockWin}!");
                return computerWin;
            }
            else if (computerSign == paper)
            {
                Print($"{prefix} {ScissorsWin}!");
                return playerWin;
            }
            else if (computerSign == scissors)
            {
                Print($"{prefix} {Tie}!");
                return tie;
            }
        }

        return tie;
    }
}
