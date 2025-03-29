public enum Sign
{
    rock,
    paper,
    scissors
    // invalid
    // Hint: Liskov substitution principle (kindof...) should we have an invalid case here?
    // so that when we map a keyboard press to a Sign, we could say oh this keyboard press is invalid.
    // this would kindof break LSP, because we added an extra case to Sign thats not really a sign.
    // It makes some sense as a Sign, but at the same time its NOT a sign.
    // rock is a sign, paper is a sign, scissors is a sign, invalid is not.
    // the bug this would lead to, is when we want to generate a random sign using
    // Array values = Enum.GetValues(typeof(Sign)); - then it would also include the invalid.
}

public enum OutCome
{
    playerWin,
    computerWin,
    tie,
    inProgress
}

public struct RockPaperScissorsConsts
{
    public static string RockWin = "Rock crushes scissors";
    public static string PaperWin = "Paper covers rock";
    public static string ScissorsWin = "Scissors cut paper";
    public static string Tie = "Tie";
    public static string ComputerWinsMatch = "Computer wins the match! Better luck next time!";
    public static string PlayerWinsMatch = "You win the match! Congratulations!";
}