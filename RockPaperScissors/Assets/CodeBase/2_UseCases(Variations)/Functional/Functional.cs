using System.Collections.Generic;
using System;

public static class FuncUtils
{
    public static T Next<T>(this IRandom random, IReadOnlyList<T> list)
    {
        if (list == null || list.Count == 0)
            throw new ArgumentException("List cannot be null or empty.", nameof(list));

        return list[random.Next(list.Count)];
    }

    public static (string result, Func<(int, int), (int, int)> scoreUpdate) GetRoundResult(string player, string computer, Dictionary<string, string> rules)
    {
        if (player == computer)
            return ("Tie!", score => score);

        if (rules[player] == computer)
            return ("You win this round!", score => (score.Item1 + 1, score.Item2));

        return ("Computer wins this round!", score => (score.Item1, score.Item2 + 1));
    }

    public static List<string> GetRoundAnnouncements(string player, string computer, string result, (int player, int computer) currentScores)
    {
        return new() { $"Player: {player}, Computer: {computer}", $"{result} Score: {currentScores.player} - {currentScores.computer}" };
    }

    public static string CheckGameEnd((int p, int c) scores, int maxTurn)
    {
        return (scores.p >= maxTurn) ? "Congratulations! You win the game!" : 
            (scores.c >= maxTurn) ? "Computer wins the game! Better luck next time." : 
            "";
    }
}

public class Functional : Variation
{
    private readonly List<string> rulesToPrint = new() {
        "Welcome to Rock-Paper-Scissors! First to 5 wins.",
        "Use 'r' for Rock, 'p' for Paper, 's' for Scissors."
    };
    private readonly IRandom _random;
    private readonly Dictionary<string, string> _rules;
    private readonly List<string> _signs = new() { "r", "p", "s" };
    private (int player, int computer) _score;
    private int _maxTurn = 5;

    public Functional(IPrinter printer) : base(printer)
    {
        _random = ServiceProvider.Random;
        _rules = new Dictionary<string, string>
        {
            { "r", "s" }, // Rock beats Scissors
            { "p", "r" }, // Paper beats Rock
            { "s", "p" }  // Scissors beats Paper
        };
    }

    public Functional(IPrinter printer, IRandom random, Dictionary<string, string> rules, List<string> signs, int maxTurn) : base(printer)
    {
        _random = random ?? ServiceProvider.Random;
        _rules = rules;
        _signs = signs;
        _maxTurn = maxTurn;
    }

    public override void Start()
    {
        rulesToPrint.ForEach(printer.Print); // Hint: a little functional... nice :D
    }

    public override void DidPressKey(string key)
    {
        if (!_signs.Contains(key))
        {
            printer.Print("Invalid input! Use 'r', 'p', or 's'.");
            return;
        }

        var computerKey = _random.Next(_signs); // Fun but may be confusing...
        
        var result = FuncUtils.GetRoundResult(key, computerKey, _rules);

        _score = result.scoreUpdate(_score);
        
        FuncUtils.GetRoundAnnouncements(key, computerKey, result.result, _score)
            .ForEach(printer.Print);

        if (FuncUtils.CheckGameEnd(_score, _maxTurn) is { Length: > 0 } gameEndText) // bruh...
        {
            printer.Print(gameEndText);
            _score = (0, 0);
        }
    }
}
