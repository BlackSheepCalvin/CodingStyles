using System;
using System.Linq;
using NUnit.Framework;
using static RockPaperScissorsConsts;

[TestFixture]
public class TDDBetterTests
{
    private MockPrinter mockPrinter;
    private MockRandom mockRandom;
    private Variation sut;

    [SetUp]
    public void SetUp()
    {
        mockPrinter = new MockPrinter();
        mockRandom = new MockRandom();
        ServiceProvider.Random = mockRandom;
    }

    [Test]
    public void TestDataDriven()
    {
        CheckVariationTests(printer => new DataDrivenProgramming(printer));
    }

    [Test]
    public void TestFunctional()
    {
        CheckVariationTests(printer => new Functional(printer));
    }

    [Test]
    public void TestSimplestOne()
    {
        CheckVariationTests(printer => new SimplestOne(printer));
    }

    [Test]
    public void TestTDDBetter()
    {
        CheckVariationTests(printer => new TDDBetter(printer));
    }

    private void CheckVariationTests<T>(Func<IPrinter, T> factory) where T : Variation
    {
        Runtest(factory, StartAnnouncesRules);
        Runtest(factory, PlayerPressesWrongKey);
        Runtest(factory, PlayerPresses_LowerCaseR);
        Runtest(factory, PlayerPresses_UpperCaseR);
        Runtest(factory, TieRound);
        Runtest(factory, PlayerWinsRound);
        Runtest(factory, ComputerWinsRound);
        Runtest(factory, PlayerWinsComplexMatch);
        Runtest(factory, ComputerWinsMatch_ThenContinuePlaying);
    }
    private void Runtest<T>(Func<IPrinter, T> factory, Action testAction) where T : Variation
    {
        sut = factory(mockPrinter);
        mockPrinter.printCallHistory.Clear();
        testAction();
    }

    private void StartAnnouncesRules()
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
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);
    }

    private void PlayerPressesWrongKey()
    {
        sut.Start();
        sut.DidPressKey("2");

        Assert.AreEqual(7, mockPrinter.printCallHistory.Count);
    }

    private void PlayerPresses_LowerCaseR()
    {
        sut.Start();
        sut.DidPressKey("r");

        var i = 7;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
    }

    private void PlayerPresses_UpperCaseR()
    {
        sut.Start();
        sut.DidPressKey("R");

        var i = 7;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
    }

    private void TieRound()
    {
        sut.Start();
        mockRandom.output = 0;
        sut.DidPressKey("R");

        var i = 7;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(Tie, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);
    }

    private void PlayerWinsRound()
    {
        sut.Start();
        mockRandom.output = 2;
        sut.DidPressKey("R");

        var i = 7;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: scissors", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(RockWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 1, C: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);
    }

    private void ComputerWinsRound()
    {
        sut.Start();
        mockRandom.output = 1;
        sut.DidPressKey("R");

        var i = 7;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 1", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);
    }

    private void PlayerWinsComplexMatch()
    {
        sut.Start();
        mockRandom.output = 1;
        sut.DidPressKey("P");

        var i = 7;
        Assert.AreEqual("Player: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(Tie, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("s");
        Assert.AreEqual("Player: scissors", mockPrinter.printCallHistory[i++]); // Hint: should you be smart, and write a private helper method to make this part easier?
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]); // Like: testNextRound(player: "r", computer: 1), or even: testNextRound(player: .rock, computer: .paper)
        Assert.AreEqual(ScissorsWin, mockPrinter.printCallHistory[i++]); // So you'd implement the logic again? :D Probably its better to just keep copy pasteing.
        Assert.AreEqual("Score: P: 1, C: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("s");
        Assert.AreEqual("Player: scissors", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(ScissorsWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 2, C: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        mockRandom.output = 0;
        sut.DidPressKey("s");
        Assert.AreEqual("Player: scissors", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(RockWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 2, C: 1", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("p");
        Assert.AreEqual("Player: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 3, C: 1", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("p");
        Assert.AreEqual("Player: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 4, C: 1", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        mockRandom.output = 1;
        sut.DidPressKey("s");
        Assert.AreEqual("Player: scissors", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(ScissorsWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 5, C: 1", mockPrinter.printCallHistory[i++]);

        Assert.AreEqual("You won the match! Congrats!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);
    }

    private void ComputerWinsMatch_ThenContinuePlaying()
    {
        sut.Start();
        mockRandom.output = 1;
        sut.DidPressKey("R");

        var i = 7;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 1", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 2", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 3", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 4", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 5", mockPrinter.printCallHistory[i++]);

        Assert.AreEqual("Computer wins! Better luck next time!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: paper", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PaperWin, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Score: P: 0, C: 1", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);
    }
}
