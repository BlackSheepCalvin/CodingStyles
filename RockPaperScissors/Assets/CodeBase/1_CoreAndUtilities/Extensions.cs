using System;

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
}