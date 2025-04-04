using System.Collections.Generic;
using static OutCome;
using System;
using System.Linq;

public class DDPGameRound: PrinterUser
{
    public OutCome OutCome;
    private GameData data;
    private IRandom random = ServiceProvider.Random;
    
    public DDPGameRound(IPrinter printer, GameData data) : base(printer)
    {
        this.data = data;
    }

    public void EvaluatePlayerSign(string playerSign)
    {
        var computerSign = GetRandomSign(data.signs);
        var prefix = $"You showed {playerSign}! Computer showed {computerSign}! -";
        if (computerSign == playerSign)
        {
            Print($"{prefix} {RockPaperScissorsConsts.Tie}!");
            OutCome = tie;
            return;
        }

        Rule playerWinsRule = data.rules
            .FirstOrDefault(rule => rule.winner == playerSign && rule.loser == computerSign);

        if (!string.IsNullOrEmpty(playerWinsRule.winner))
        {
            Print($"{prefix} {playerWinsRule.ToString().CapitalizeFirst()}!");
            OutCome = playerWin;
            return;
        }

        Rule computerWinsRule = data.rules
            .FirstOrDefault(rule => rule.winner == computerSign && rule.loser == playerSign);

        if (!string.IsNullOrEmpty(computerWinsRule.winner))
        {
            Print($"{prefix} {computerWinsRule.ToString().CapitalizeFirst()}!");
            OutCome = computerWin;
            return;
        }

        OutCome = tie;
        Print($"Error: couldn't find rule"); // Hint: not sure what the best way to handle this. running a datavalidation script on data after json parsing could be one way.
                                             // But we make the error apparent so if there is a bug, it can be investigated, and the game won't pretend that everything is fine.
    }

    public string GetRandomSign(List<string> list)
    {
        if (list == null || list.Count == 0)
            return "";

        int randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }
}
