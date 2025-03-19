using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using static Sign;

public class TDDGameRoundTests
{
    private MockPrinter mockPrinter;
    private MockRandom mockRandom;
    private TDDGameRound sut;

    [SetUp]
    public void SetUp()
    {
        mockPrinter = new MockPrinter();
        mockRandom = new MockRandom();
        sut = new TDDGameRound(mockPrinter, mockRandom);
    }

    [Test]
    public void PlayerPresses_R_ComputerRollsRock()
    {
        mockRandom.output = 0;

        sut.EvaluatePlayerSign(rock);

        var i = 0;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Tie!", mockPrinter.printCallHistory[i++]);
    }

    [Test]
    public void PlayerPresses_S_ComputerRollsRock()
    {
        mockRandom.output = 0;

        sut.EvaluatePlayerSign(scissors);

        var i = 0;
        Assert.AreEqual("Player: scissors", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Rock smashes scissors!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer wins!", mockPrinter.printCallHistory[i++]);
    }

    [Test]
    public void PlayerPresses_S_ComputerRollsScissors()
    {
        mockRandom.output = 2;

        sut.EvaluatePlayerSign(scissors);

        var i = 0;
        Assert.AreEqual("Player: scissors", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: scissors", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Tie!", mockPrinter.printCallHistory[i++]);
    }

    [Test]
    public void P_R() // scissor scissor paper rock
    {
        mockRandom.output = 0;
        sut.EvaluatePlayerSign(paper);

        var i = 0;
        Assert.AreEqual("Player: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Paper covers rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Player wins!", mockPrinter.printCallHistory[i++]);
    }
}
