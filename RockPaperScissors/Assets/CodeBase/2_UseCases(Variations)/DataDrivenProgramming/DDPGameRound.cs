using System.Collections.Generic;
using static OutCome;
using System;
using System.Linq;

public class DDPGameRound: PrinterUser
{
    public OutCome OutCome;
    private GameData data;
    private Random random = new Random();
    
    public DDPGameRound(IPrinter printer, GameData data) : base(printer)
    {
        this.data = data;
    }

    public void EvaluatePlayerSign(string playerSign)
    {
        var computerSign = GetRandomSign(data.signs);
        Print($"Computer: {computerSign}");
        Print($"Player: {playerSign}");
        if (computerSign == playerSign)
        {
            Print("Tie!");
            OutCome = tie;
            return;
        }

        Rule playerWinsRule = data.rules
            .FirstOrDefault(rule => rule.winner == playerSign && rule.loser == computerSign);

        if (!string.IsNullOrEmpty(playerWinsRule.winner))
        {
            Print(playerWinsRule.ToString());
            Print("You win!");
            OutCome = playerWin;
            return;
        }

        Rule computerWinsRule = data.rules
            .FirstOrDefault(rule => rule.winner == computerSign && rule.loser == playerSign);

        if (!string.IsNullOrEmpty(computerWinsRule.winner))
        {
            Print(computerWinsRule.ToString());
            Print("Computer wins!");
            OutCome = computerWin;
            return;
        }

        OutCome = tie;
        Print("Tie!");
    }

    public string GetRandomSign(List<string> list)
    {
        if (list == null || list.Count == 0)
            return "";

        int randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }
}
