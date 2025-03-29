using static OutCome;
public class DataDrivenProgramming : Variation
{
    IDataProvider dataProvider;
    KeyPressInterpreter keyPressInterpreter;
    DDPGameMatch gameMatch;

    public DataDrivenProgramming(IPrinter printer) : base(printer)
    {
        dataProvider = ServiceProvider.DataProvider;
    }

    public override void Start()
    {
        dataProvider.RequestData<GameData>("data", data => {
            keyPressInterpreter = ServiceProvider.KeyPressInterpreterFactory(data.signs);
            gameMatch = new DDPGameMatch(printer, data);
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
                printer.Print("Congratulations! You won the match!");
                break;
            case computerWin:
                printer.Print("Better luck next time! Computer won the match!");
                break;
            case tie:
                printer.Print($"Aaand tie... somehow!");
                break;
            case inProgress:
                printer.Print($"Computer: {gameMatch.ComputerScore}");
                printer.Print($"Player: {gameMatch.PlayerScore}");
                break;
        }
        AnnounceNextRound();
    }

    void AnnounceNextRound()
    {
        printer.Print("");
        printer.Print("3... 2... 1...");
    }

    void StartWithData(GameData data)
    {
        printer.Print(FormatRulesDescription(data));
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

