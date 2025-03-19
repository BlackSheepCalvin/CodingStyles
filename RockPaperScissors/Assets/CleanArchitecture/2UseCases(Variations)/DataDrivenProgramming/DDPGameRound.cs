using System.Collections.Generic;
using static OutCome;
using System;
using System.Linq;

public class DDPGameRound
{
    public OutCome OutCome;
    private GameData data;
    private Random random = new Random();
    private IPrinter printer;

    public DDPGameRound(IPrinter printer, GameData data)
    {
        this.data = data;
        this.printer = printer;
    }

    public void EvaluatePlayerSign(string playerSign)
    {
        var computerSign = GetRandomSign(data.signs);
        printer.Print($"Computer: {computerSign}");
        printer.Print($"Player: {playerSign}");
        if (computerSign == playerSign)
        {
            printer.Print("Tie!");
            OutCome = tie;
            return;
        }

        Rule playerWinsRule = data.rules
            .FirstOrDefault(rule => rule.winner == playerSign && rule.loser == computerSign);

        if (!string.IsNullOrEmpty(playerWinsRule.winner))
        {
            printer.Print(playerWinsRule.ToString());
            printer.Print("You win!");
            OutCome = playerWin;
            return;
        }

        Rule computerWinsRule = data.rules
            .FirstOrDefault(rule => rule.winner == computerSign && rule.loser == playerSign);

        if (!string.IsNullOrEmpty(computerWinsRule.winner))
        {
            printer.Print(computerWinsRule.ToString());
            printer.Print("Computer wins!");
            OutCome = computerWin;
            return;
        }

        OutCome = tie;
        printer.Print("Tie!");
    }

    public string GetRandomSign(List<string> list)
    {
        if (list == null || list.Count == 0)
            return "";

        int randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }
}
