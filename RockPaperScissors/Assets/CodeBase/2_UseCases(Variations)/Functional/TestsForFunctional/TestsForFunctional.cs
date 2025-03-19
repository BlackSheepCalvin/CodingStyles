using NUnit.Framework;
using System;
using System.Collections.Generic;

public class NextTests
{
    [Test]
    public void ReturnsElementFromIntList()
    {
        var mockRandom = new MockRandom();
        mockRandom.output = 3;
        var list = new List<int> { 1, 2, 3, 4, 5 };

        var result = mockRandom.Next(list);
        
        Assert.AreEqual(4, result);
    }

    [Test]
    public void ReturnsElementFromStringList()
    {
        var mockRandom = new MockRandom();
        mockRandom.output = 0;
        var list = new List<string> { "egy", "ketto"};

        var result = mockRandom.Next(list);

        Assert.AreEqual("egy", result);
    }

    [Test]
    public void ThrowsException_WhenListIsEmpty()
    {
        var mockRandom = new MockRandom();
        var emptyList = new List<int>();
        Assert.Throws<ArgumentException>(() => mockRandom.Next(emptyList));
    }
}

public class GetRoundResultTests
{
    [Test]
    public void GetRoundResultTestsSimplePasses()
    {
        var rules = new Dictionary<string, string>
        {
            { "r", "s" },
            { "p", "r" },
            { "s", "p" } 
        };

        var result = FuncUtils.GetRoundResult("r", "r", rules);
        Assert.AreEqual("Tie!", result.result);
        Assert.AreEqual((0, 0), result.scoreUpdate((0, 0)));

        result = FuncUtils.GetRoundResult("r", "p", rules);
        Assert.AreEqual("Computer wins this round!", result.result);
        Assert.AreEqual((0, 1), result.scoreUpdate((0, 0)));

        result = FuncUtils.GetRoundResult("s", "p", rules);
        Assert.AreEqual("You win this round!", result.result);
        Assert.AreEqual((1, 0), result.scoreUpdate((0, 0)));
    }
}

public class GetRoundAnnouncementsTests
{
    [Test]
    public void GetRoundAnnouncements_ReturnsCorrectFormat()
    {
        var announcements = FuncUtils.GetRoundAnnouncements("r", "p", "ResultString", (2, 3));
        Assert.AreEqual(2, announcements.Count);
        Assert.AreEqual("Player: r, Computer: p", announcements[0]);
        Assert.AreEqual("ResultString Score: 2 - 3", announcements[1]);
    }
}

public class CheckGameEndTests
{
    [Test]
    public void CheckGameEnd_PlayerWins_ReturnsWinMessage()
    {
        var result = FuncUtils.CheckGameEnd((5, 3), 5);
        Assert.AreEqual("Congratulations! You win the game!", result);
    }

    [Test]
    public void CheckGameEnd_ComputerWins_ReturnsLoseMessage()
    {
        var result = FuncUtils.CheckGameEnd((0, 2), 2);
        Assert.AreEqual("Computer wins the game! Better luck next time.", result);
    }

    [Test]
    public void CheckGameEnd_NoWinner_ReturnsEmptyString()
    {
        var result = FuncUtils.CheckGameEnd((5, 3), 6);
        Assert.AreEqual("", result);
    }
}