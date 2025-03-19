using System;
using System.Linq;
using NUnit.Framework;
using static Sign;
using static OutCome;
// create a rock-paper-scissors game, where the player plays against the computer(random), and the first to score 5 wins the game.

// iteration 1: the player plays against the computer(random)
/* 
* Player: (r)ock, Computer: (s)cissor
* Rock beats scissor! score: 1 - 0
* Player: (r)ock, Computer: (r)ock
* Tie! score: 1 - 0
* Player: (p)aper, Computer: (r)ock
* score: 2 - 0...

*Player: (s)cissor, Computer: (r)ock
* score: 2 - 5
* Computer wins!*/

public class TestDrivenDevelopmentTests
{
    private MockPrinter mockPrinter;
    private MockTDDGameMatch mockGameMatch;
    private TestDrivenDevelopment sut;

    [SetUp]
    public void SetUp()
    {
        mockPrinter = new MockPrinter();
        mockGameMatch = new MockTDDGameMatch();
        sut = new TestDrivenDevelopment(mockPrinter, mockGameMatch);
    }

    [Test]
    public void StartAnnouncesRules()
    {
        Assert.AreEqual(null, mockPrinter.printCallHistory.FirstOrDefault());

        sut.Start();

        Assert.IsNotNull(sut);
        var i = 0;
        Assert.AreEqual("Welcome to rock paper scissors!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("The rules are:", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Rock smashes scissors", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Scissors cuts paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Paper covers rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(true, mockGameMatch.didCallAnnounceNextRound);
    }

    [Test]
    public void PlayerPressesWrongKey()
    {
        sut.Start();
        sut.DidPressKey("2");

        Assert.AreEqual(null, mockGameMatch.didCallEvaluatePlayerSignWith);
    }

    [Test]
    public void PlayerPresses_LowerCaseR()
    {
        sut.Start();
        sut.DidPressKey("r");

        Assert.AreEqual(rock, mockGameMatch.didCallEvaluatePlayerSignWith);
    }

    [Test]
    public void PlayerPresses_UpperCaseR_InProgress()
    {
        sut.Start();
        mockGameMatch.OutCome = inProgress;
        mockGameMatch.PlayerScore = 3;
        mockGameMatch.ComputerScore = 2;
        sut.DidPressKey("R");

        Assert.AreEqual(rock, mockGameMatch.didCallEvaluatePlayerSignWith);
        var i = 6;
        Assert.AreEqual("score: 3 - 2", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(true, mockGameMatch.didCallAnnounceNextRound);
    }

    [Test]
    public void GameOver_PlayerWin()
    {
        sut.Start();
        mockGameMatch.didCallAnnounceNextRound = false;
        mockGameMatch.OutCome = playerWin;
        sut.DidPressKey("R");

        var i = 6;
        Assert.AreEqual("You won the match! Congrats!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(true, mockGameMatch.didCallAnnounceNextRound);
    }
}
