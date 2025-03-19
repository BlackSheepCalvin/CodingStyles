using System;
using System.Collections.Generic;

public class MockRandom : IRandom
{
    public int output = 0;

    public int Next(int maxValue)
    {
        return output;
    }
}
