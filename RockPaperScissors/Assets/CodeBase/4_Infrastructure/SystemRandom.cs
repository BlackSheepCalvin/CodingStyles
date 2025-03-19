using System;

public class SystemRandom : IRandom
{
    private readonly Random _random;
    public SystemRandom()
    {
        _random = new Random();
    }
    public int Next(int max)
    {
        return _random.Next(max);
    }
}
