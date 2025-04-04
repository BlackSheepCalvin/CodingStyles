using System;
using System.Collections.Generic;
using System.Linq;

public static class RandomExtensions
{
    public static Sign RandomSign(this IRandom random)
    {
        Array values = Enum.GetValues(typeof(Sign)); // Get all enum values
        return (Sign)values.GetValue(random.Next(values.Length)); // Pick a random one
    }
}

public static class StringExtensions
{
    public static Sign? decodeSign(this string key)
    {
        switch (key.ToLower())
        {
            case "r":
                return Sign.rock;
            case "p":
                return Sign.paper;
            case "s":
                return Sign.scissors;
        }
        return null;
    }

    public static string CapitalizeFirst(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return char.ToUpper(text[0]) + text.Substring(1).ToLower();
    }

    public static List<string> ToCapitalizedFirstList(this List<string> input) // Hint: TDD / YAGNI / KISS? / Fearless developer?: should I test these?
    {                                                                   // This would be a typical problem that is nice to test. But it is also simple, and highly unlikely to ever change, so what would you gain?
                                                                        // But then again, unit tests are cheap... its really a prefrence, i think.
        if (input == null || input.Count == 0)
            return new List<string>();
        return input.Select(x => x.CapitalizeFirst()).ToList();
    }
}