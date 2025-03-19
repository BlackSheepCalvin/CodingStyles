public class MockTDDGameMatch : ITDDGameMatch
{
    public OutCome OutCome { get; set; }
    public int PlayerScore { get; set; }
    public int ComputerScore { get; set; }

    public bool didCallAnnounceNextRound = false;
    public void AnnounceNextRound()
    {
        didCallAnnounceNextRound = true;
    }

    public Sign? didCallEvaluatePlayerSignWith = null;
    public void EvaluatePlayerSign(Sign playerSign)
    {
        didCallEvaluatePlayerSignWith = playerSign;
    }
}
