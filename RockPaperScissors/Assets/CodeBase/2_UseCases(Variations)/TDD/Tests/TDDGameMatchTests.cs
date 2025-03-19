using NUnit.Framework;
using static Sign;
using static OutCome;

public class TDDGameMatchTests
{
    private MockPrinter mockPrinter;
    private MockTDDGameRound mockGameRound;
    private TDDGameMatch sut;

    [SetUp]
    public void SetUp()
    {
        mockPrinter = new MockPrinter();
        mockGameRound = new MockTDDGameRound();
        sut = new TDDGameMatch(mockPrinter, mockGameRound, 2);
    }

    [Test]
    public void RR_PS_RP()
    {
        Assert.AreEqual(inProgress, sut.OutCome);
        mockGameRound.LastOutCome = tie;
        sut.EvaluatePlayerSign(rock);

        Assert.AreEqual(rock, mockGameRound.didCallEvaluatePlayerSignWith);

        mockGameRound.LastOutCome = computerWin;
        sut.EvaluatePlayerSign(paper);
        Assert.AreEqual(paper, mockGameRound.didCallEvaluatePlayerSignWith);
        Assert.AreEqual(inProgress, sut.OutCome);

        mockGameRound.LastOutCome = computerWin;
        sut.EvaluatePlayerSign(rock);
        Assert.AreEqual(computerWin, sut.OutCome);
    }
}
