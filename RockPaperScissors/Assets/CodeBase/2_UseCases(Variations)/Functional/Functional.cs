using System.Collections.Generic;
using System;
using static RockPaperScissorsConsts;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;
using GluonGui.Dialog;

public static class FuncUtils
{
    public static T Next<T>(this IRandom random, IReadOnlyList<T> list)
    {
        if (list == null || list.Count == 0)
            throw new ArgumentException("List cannot be null or empty.", nameof(list));

        return list[random.Next(list.Count)];
    }

    public static (string result, Func<(int, int), (int, int)> scoreUpdate) GetRoundResult(string player, string computer, Dictionary<string, string> rules, Dictionary<string, string> signToString)
    {
        var prefix = $"You showed {signToString[player]}! Computer showed {signToString[computer]}! - ";
        if (player == computer)
            return ($"{prefix}{Tie}!", score => score);

        var ruleKey = $"{player}{computer}";
        if (rules.ContainsKey(ruleKey))
            return ($"{prefix}{rules[ruleKey]}!", score => (score.Item1 + 1, score.Item2)); // Hint: we are returning in the middle but looking at the whole func, i dont think this is too misleading...
                                                                                    // obviously it is a check and return type of function.

        ruleKey = $"{computer}{player}";
        if (rules.ContainsKey(ruleKey))
            return ($"{prefix}{rules[ruleKey]}!", score => (score.Item1, score.Item2 + 1));

        return ($"{prefix}{Tie}!", score => score);
    }

    public static List<string> GetRoundAnnouncements(string result, (int player, int computer) currentScores)
    {
        return new() { result, $"Computer: {currentScores.computer}, Player: {currentScores.player}" };
    }

    public static string CheckGameEnd((int p, int c) scores, int maxTurn)
    {
        return (scores.p >= maxTurn) ? PlayerWinsMatch : 
            (scores.c >= maxTurn) ? ComputerWinsMatch : 
            "";
    }
}

public class Functional : Variation
{
    private readonly IRandom _random;
    private readonly Dictionary<string, string> _rules;
    private readonly List<string> _signs = new() { "r", "p", "s" };
    private readonly Dictionary<string, string> _signToString = new() { { "r", "rock" }, { "p", "paper" }, { "s", "scissors" } };
    private (int player, int computer) _score;
    private int _maxTurn = 5;
    private int _badKeyCounter;

    public Functional(IPrinter printer) : base(printer)
    {
        _random = ServiceProvider.Random;
        _rules = new Dictionary<string, string>
        {
            { "rs", RockWin },
            { "pr", PaperWin },
            { "sp", ScissorsWin } 
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
        Rules.ForEach(Print);
    }

    public override void DidPressKey(string key)
    {
        if (!_signs.Contains(key.ToLower()))
        {
            _badKeyCounter++;
            if (_badKeyCounter == 3) { Print(OnInvalidKey); }
            return; // Hint: it is a good practice to return at the beginning of the method, or only return at the very end. Returning in the middle is frowned upon because it is easier to miss that return.
        }

        _badKeyCounter = 0;

        var computerKey = _random.Next(_signs); // Fun but may be confusing...
        
        var result = FuncUtils.GetRoundResult(key.ToLower(), computerKey, _rules, _signToString);

        _score = result.scoreUpdate(_score);
        
        FuncUtils.GetRoundAnnouncements(result.result, _score)
            .ForEach(Print);

        if (FuncUtils.CheckGameEnd(_score, _maxTurn) is { Length: > 0 } gameEndText) // bruh... // bruh from the future too... wtf is this? :D
        {
            Print(gameEndText);
            Print(NextMatchAnnouncement);
            _score = (0, 0);
        }
        Print(NextRoundAnnouncement);
    }
}
