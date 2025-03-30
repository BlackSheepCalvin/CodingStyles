using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static RockPaperScissorsConsts;

[TestFixture]
public class TDDBetterTests
{
    private MockPrinter mockPrinter;
    private MockRandom mockRandom;
    private Variation sut; // sut == System Under Test
    private int firstRoundEntryIndex = Rules.Count;

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
        RunTests(x => new DataDrivenProgramming(x));
    }

    [Test]
    public void TestFunctional()
    {
        RunTests(x => new Functional(x));
    }

    [Test]
    public void TestSimplestOne()
    {
        RunTests(x => new SimplestOne(x));
    }

    [Test]
    public void TestTDDBetter()
    {
        RunTests(x => new TDDBetter(x));
    }

    [Test]
    public void TestEventsVariaton()
    {
        RunTests(x => new EventsVariation(x));
    }

    private void RunTests<T>(Func<IPrinter, T> factory) where T : Variation
    {
        var testList = new List<Action>() {
            StartAnnouncesRules,
            PlayerPressesWrongKey,
            PlayerPressesWrongKey3Times,
            PlayerPressesWrongKey2Times_ThenGoodKey_ThenWrongAgain,
            PlayerPresses_LowerCaseR,
            PlayerPresses_UpperCaseR,
            TieRound,
            PlayerWinsRound,
            ComputerWinsRound,
            PlayerWinsComplexMatch,
            ComputerWinsMatch_ThenContinuePlaying
        };
        testList.ForEach(x =>
        {
            sut = factory(mockPrinter);
            mockPrinter.printCallHistory.Clear();
            x();
        });
    }

    private void StartAnnouncesRules()
    {
        sut.Start();

        Assert.IsNotNull(sut);
        CollectionAssert.AreEqual(Rules, mockPrinter.printCallHistory);
    }

    private void PlayerPressesWrongKey()
    {
        sut.Start();
        sut.DidPressKey("2");

        Assert.AreEqual(Rules.Count, mockPrinter.printCallHistory.Count); // no extra text yet about pressing wrong key.
        // Hint: i think Rules.Count is better than firstRoundEntryIndex. It captures the idea better.
        // Hint: Commenting: sometimes it's ok to comment, if something is a bit harder to understand.
    }

    private void PlayerPressesWrongKey3Times()
    {
        sut.Start();
        sut.DidPressKey("2");
        sut.DidPressKey("2");
        sut.DidPressKey("2");

        Assert.AreEqual(OnInvalidKey, mockPrinter.printCallHistory[Rules.Count]);
    }

    private void PlayerPressesWrongKey2Times_ThenGoodKey_ThenWrongAgain()
    {
        sut.Start();
        sut.DidPressKey("2");
        sut.DidPressKey("2");
        sut.DidPressKey("r");
        sut.DidPressKey("2");

        var i = firstRoundEntryIndex;
        Assert.AreEqual("You showed rock! Computer showed rock! - Tie!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 0, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(Rules.Count + 3, mockPrinter.printCallHistory.Count); // no extra text about pressing wrong key, because there was a good one in-between.
    }

    private void PlayerPresses_LowerCaseR()
    {
        sut.Start();
        sut.DidPressKey("r");

        var i = firstRoundEntryIndex; // Hint: Magic Numbers: /sarcasm on: Oh my god, i had the magic number 7 here before this!!!
                                      // I was so wrong, it was horrible! Now i see that i should never ever use magic numbers, ever again!
                                      // I had to press ctrl+f to fix this, it was very painful.
        Assert.AreEqual("You showed rock! Computer showed rock! - Tie!", mockPrinter.printCallHistory[i++]); // Hint: i means index, btw. I think its ok to use 1 letter variables in rare cases.
        // Hint: logic in tests / pretty code.
        // Should I implement something like mockPrinter.Next() to make mockPrinter.printCallHistory[i++] prettier?
        // You shouldnt have too much logic in tests (because you are writing a test, not a program. And if you have logic in your test, who is gonna test your tests?!)
        // but then again... if it makes things easier, go ahead...
        // But would mockPrinter.Next() or even printer.Next() be better? - i'd argue no - at least currently.
        // looking at mockPrinter.printCallHistory[i++], i know exactly what is going on. Looking at printer.Next() i'd have little idea. Is printer a mock? How does Next work exactly?
        // encapsulation is good if you dont want to know what is happening. But bad if you do want to know what is happening.
    }

    private void PlayerPresses_UpperCaseR()
    {
        sut.Start();
        sut.DidPressKey("P");

        var i = firstRoundEntryIndex;
        Assert.AreEqual("You showed paper! Computer showed rock! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
    }

    private void TieRound()
    {
        sut.Start();
        mockRandom.output = 0;
        sut.DidPressKey("R");

        var i = firstRoundEntryIndex;
        Assert.AreEqual("You showed rock! Computer showed rock! - Tie!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 0, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);
    }

    private void PlayerWinsRound()
    {
        sut.Start();
        mockRandom.output = 2;
        sut.DidPressKey("R");

        var i = firstRoundEntryIndex;
        Assert.AreEqual("You showed rock! Computer showed scissors! - Rock crushes scissors!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 0, Player: 1", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);
    }

    private void ComputerWinsRound()
    {
        sut.Start();
        mockRandom.output = 1;
        sut.DidPressKey("R");

        var i = firstRoundEntryIndex;
        Assert.AreEqual("You showed rock! Computer showed paper! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 1, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);
    }

    private void PlayerWinsComplexMatch()
    {
        sut.Start();
        mockRandom.output = 1;
        sut.DidPressKey("P");

        var i = firstRoundEntryIndex;
        Assert.AreEqual("You showed paper! Computer showed paper! - Tie!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 0, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("s");
        Assert.AreEqual("You showed scissors! Computer showed paper! - Scissors cut paper!", mockPrinter.printCallHistory[i++]); // Hint: should you be smart, and write a private helper method to make this part easier?
        Assert.AreEqual("Computer: 0, Player: 1", mockPrinter.printCallHistory[i++]); // Like: testNextRound(player: "r", computer: 1), or even: testNextRound(player: .rock, computer: .paper)
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]); // So you'd implement the logic again? :D Probably its better to just keep copy pasteing.

        sut.DidPressKey("s");
        Assert.AreEqual("You showed scissors! Computer showed paper! - Scissors cut paper!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 0, Player: 2", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        mockRandom.output = 0;
        sut.DidPressKey("s");
        Assert.AreEqual("You showed scissors! Computer showed rock! - Rock crushes scissors!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 1, Player: 2", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("P"); // just a little variety / redundancy. Why not.
        Assert.AreEqual("You showed paper! Computer showed rock! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 1, Player: 3", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("p");
        Assert.AreEqual("You showed paper! Computer showed rock! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 1, Player: 4", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        mockRandom.output = 1;
        sut.DidPressKey("s");
        Assert.AreEqual("You showed scissors! Computer showed paper! - Scissors cut paper!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 1, Player: 5", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(PlayerWinsMatch, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextMatchAnnouncement, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);
    }

    private void ComputerWinsMatch_ThenContinuePlaying()
    {
        sut.Start();
        mockRandom.output = 1;
        sut.DidPressKey("R");

        var i = firstRoundEntryIndex;
        Assert.AreEqual("You showed rock! Computer showed paper! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 1, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("You showed rock! Computer showed paper! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 2, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("You showed rock! Computer showed paper! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 3, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("You showed rock! Computer showed paper! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 4, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("You showed rock! Computer showed paper! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 5, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(ComputerWinsMatch, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextMatchAnnouncement, mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);

        sut.DidPressKey("R");
        Assert.AreEqual("You showed rock! Computer showed paper! - Paper covers rock!", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual("Computer: 1, Player: 0", mockPrinter.printCallHistory[i++]);
        Assert.AreEqual(NextRoundAnnouncement, mockPrinter.printCallHistory[i++]);
    }
}
