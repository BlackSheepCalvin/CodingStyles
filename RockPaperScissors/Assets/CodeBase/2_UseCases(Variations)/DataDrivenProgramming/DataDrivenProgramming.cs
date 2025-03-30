using static OutCome;
public class DataDrivenProgramming : Variation
{
    IDataProvider dataProvider;
    KeyPressInterpreter keyPressInterpreter;
    DDPGameMatch gameMatch;

    public DataDrivenProgramming(IPrinter printer) : base(printer)
    {
        dataProvider = ServiceProvider.DataProvider;
        gameMatch = new DDPGameMatch(printer);
    }

    public override void Start()
    {
        dataProvider.RequestData<GameData>("data", data => {
            keyPressInterpreter = ServiceProvider.KeyPressInterpreterFactory(data.signs);
            gameMatch.SetData(data);
            StartWithData(data);
        });
    }

    public override void DidPressKey(string key)
    {
        var sign = keyPressInterpreter.EvaluateInputKey(key);
        if (sign == "")
        {
            return;
        }
        gameMatch.EvaluatePlayerSign(sign);
        EvaluateGameState(gameMatch.OutCome);
    }

    private void EvaluateGameState(OutCome currentGameState)
    {
        switch (currentGameState)
        {
            case playerWin:
                Print("Congratulations! You won the match!");
                break;
            case computerWin:
                Print("Better luck next time! Computer won the match!");
                break;
            case tie:
                Print($"Aaand tie... somehow!");
                break;
            case inProgress:
                Print($"Computer: {gameMatch.ComputerScore}");
                Print($"Player: {gameMatch.PlayerScore}");
                break;
        }
        AnnounceNextRound();
    }

    void AnnounceNextRound()
    {
        Print("");
        Print("3... 2... 1...");
    }

    void StartWithData(GameData data)
    {
        Print(FormatRulesDescription(data));
    }

    string FormatRulesDescription(GameData gameRules)
    {
        string signsText = string.Join("-", gameRules.signs);

        string rulesText = "";
        foreach (var rule in gameRules.rules)
        {
            rulesText += $"{rule}\n";
        }

        string formattedDescription = string.Format(
            gameRules.rulesDescription,
            signsText,
            rulesText.Trim(),
            gameRules.matchLength
        );

        return formattedDescription;
    }
}

