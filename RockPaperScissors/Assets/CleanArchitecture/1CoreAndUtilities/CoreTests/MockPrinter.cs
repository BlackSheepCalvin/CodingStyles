using System.Collections.Generic;

// Hint: use Moq or some automatic mock generator for interfaces, if you can!
// It will automate the creation of mocks, and will standardize the way mocks work
// so mocks will behave more predictibly, and you can work with them without having to look into them!
public class MockPrinter : IPrinter
{
    public List<string> printCallHistory = new List<string>();
    public void Print(string message)
    {
        printCallHistory.Add(message);
    }
}
