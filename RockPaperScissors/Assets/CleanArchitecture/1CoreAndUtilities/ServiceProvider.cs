using System;
using System.Collections.Generic;

// Hint: Service Provider
// It allows you to to see all your "singletons" or static services in one place
// And it makes it easy to search for places where you are using static services (as long as you decide to always use static services through this class)
public static class ServiceProvider
{
    public static IRandom Random;

    public static IDataProvider DataProvider;
    public static Func<List<string>, KeyPressInterpreter> KeyPressInterpreterFactory { get; } =
            commands => new KeyPressInterpreter(commands);
}
