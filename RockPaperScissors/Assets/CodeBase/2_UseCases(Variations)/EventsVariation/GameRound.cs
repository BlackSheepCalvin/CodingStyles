using static RockPaperScissorsConsts;
using static Sign;
using static OutCome;
using static RPSEvent;

public class GameRound // Hint: False Decoupling, Straw man argument: (whining again, sorry):
{                      // "Nice! We DECOUPLED GameRound from PrinterUser!!! GameRound doesn't have to know about stupid details like: printing..."
                       // Well yea... but now we are coupled to the listener. And we have absolutely no idea what that thing is. Congratulations! :D
                       // strawman: "That the whole point! You dont have to know! You dont care!"
                       // Well I dont know about you, mr. strawman, usually i like to know what is happening in the codebase...
    private IRandom random = ServiceProvider.Random;
    private RPSEventListener listener;
    public OutCome LastOutCome { get; private set; }
    public GameRound(RPSEventListener listener)
    {
        this.listener = listener;
    }

    public void EvaluatePlayerSign(Sign playerSign)
    {
        listener.FireEvent(PlayerShowsSign, playerSign);
        var computerSign = random.RandomSign();
        listener.FireEvent(ComputerShowsSign, computerSign);
        if (playerSign == rock)
        {
            if (computerSign == rock)
            {
                LastOutCome = tie;
            }
            else if (computerSign == paper)
            {
                LastOutCome = computerWin;
            }
            else if (computerSign == scissors)
            {
                LastOutCome = playerWin;
            }
        }
        else if (playerSign == paper)
        {
            if (computerSign == rock)
            {
                LastOutCome = playerWin;
            }
            else if (computerSign == paper)
            {
                LastOutCome = tie;
            }
            else if (computerSign == scissors)
            {
                LastOutCome = computerWin;
            }
        }
        else if (playerSign == scissors)
        {
            if (computerSign == rock)
            {
                LastOutCome = computerWin;
            }
            else if (computerSign == paper)
            {
                LastOutCome = playerWin;
            }
            else if (computerSign == scissors)
            {
                LastOutCome = tie;
            }
        }
    }
}
