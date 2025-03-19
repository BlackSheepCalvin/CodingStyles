using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OutCome;

public interface ITDDGameMatch
{
    OutCome OutCome { get; }
    public int PlayerScore { get; }
    public int ComputerScore { get; }
    void AnnounceNextRound();
    void EvaluatePlayerSign(Sign playerSign);
}

public class TDDGameMatch : ITDDGameMatch
{
    public int PlayerScore { get; private set; }
    public int ComputerScore { get; private set; }
    int targetScore = 5;
    IPrinter printer;
    ITDDGameRound gameRound;
    public OutCome OutCome { get; private set; }

    public TDDGameMatch(IPrinter printer, ITDDGameRound gameRound, int targetScore)
    {
        this.printer = printer;
        this.targetScore = targetScore;
        this.gameRound = gameRound;
        OutCome = inProgress;
    }

    public void AnnounceNextRound()
    {
        printer.Print("3... 2... 1...");
    }

    public void EvaluatePlayerSign(Sign playerSign)
    {
        gameRound.EvaluatePlayerSign(playerSign);
        switch (gameRound.LastOutCome)
        {
            case computerWin:
                ComputerScore++;
                break;
            case playerWin:
                PlayerScore++;
                break;
            case tie:
                break;
        }
        OutCome = CurrentGameState();
        if (OutCome != inProgress)
        {
            PlayerScore = 0;
            ComputerScore = 0;
        }
    }

    OutCome CurrentGameState()
    {
        if (PlayerScore >= targetScore && ComputerScore >= targetScore)
        {
            return tie;
        }

        if (PlayerScore >= targetScore)
        {
            return playerWin;
        }

        if (ComputerScore >= targetScore)
        {
            return computerWin;
        }

        return inProgress;
    }
}
