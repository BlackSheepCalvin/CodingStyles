using NUnit.Framework;
using System;
using System.Collections.Generic;


[TestFixture]
public class FunctionalClassTests
{
    private MockPrinter _mockPrinter;
    private MockRandom _mockRandom;
    private Dictionary<string, string> _rules;
    private List<string> _signs;

    [SetUp]
    public void Setup()
    {
        _mockRandom = new MockRandom();
        _rules = new Dictionary<string, string>
        {
            { "r", "s" },
            { "p", "r" },
            { "s", "p" }
        };
        _signs = new List<string> { "r", "p", "s" };
        _mockPrinter = new MockPrinter();
    }

    [Test]
    public void Functional_DidPressKey_ValidInput_PrintsCorrectOutput()
    {
        var functional = new Functional(_mockPrinter, _mockRandom, _rules, _signs, 5);

        _mockRandom.output = 1;
        functional.DidPressKey("r");
        
        var i = 0;
        Assert.AreEqual("Player: r, Computer: p", _mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer wins this round! Score: 0 - 1", _mockPrinter.printCallHistory[i++]);
    }
}