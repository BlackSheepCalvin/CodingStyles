using System.Linq;
using NUnit.Framework;
using static RockPaperScissorsConsts;

public class TDDBetterTests
{
    private MockPrinter mockPrinter;
    private MockRandom mockRandom;
    private TDDBetter sut;

    [SetUp]
    public void SetUp()
    {
        mockPrinter = new MockPrinter();
        mockRandom = new MockRandom();
        ServiceProvider.Random = mockRandom;
        sut = new TDDBetter(mockPrinter);
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
        Assert.AreEqual("3...2..1..", mockPrinter.printCallHistory[i++]);
    }

    [Test]
    public void PlayerPressesWrongKey()
    {
        sut.Start();
        sut.DidPressKey("2");

        Assert.AreEqual(7, mockPrinter.printCallHistory.Count);
    }

    [Test]
    public void PlayerPresses_LowerCaseR()
    {
        sut.Start();
        sut.DidPressKey("r");

        var i = 7;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
    }

    [Test]
    public void PlayerPresses_UpperCaseR()
    {
        sut.Start();
        sut.DidPressKey("R");

        var i = 7;
        Assert.AreEqual("Player: rock", mockPrinter.printCallHistory[i++]);
    }

    [Test]
    public void TieRound()
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

    [Test]
    public void PlayerWinsRound()
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

    [Test]
    public void ComputerWinsRound()
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

    [Test]
    public void PlayerWinsComplexMatch()
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

    [Test]
    public void ComputerWinsMatch_ThenContinuePlaying()
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
