using System.Collections.Generic;
using System.Reflection;
using static OutCome;
using static RockPaperScissorsConsts;
public class DataDrivenProgramming : Variation
{
    private IDataProvider dataProvider;
    private KeyPressInterpreter keyPressInterpreter;
    private DDPGameMatch gameMatch;
    private int badKeyCounter;
    private List<string> rulesDescriptionToList;
    private string invalidKeyMessage;

    public DataDrivenProgramming(IPrinter printer) : base(printer)
    {
        dataProvider = ServiceProvider.DataProvider;
    }

    public override void Start()
    {
        dataProvider.RequestData<GameData>("data", data => {
            keyPressInterpreter = ServiceProvider.KeyPressInterpreterFactory(data.signs);
            gameMatch = new DDPGameMatch(printer, data);
            rulesDescriptionToList = data.RulesDescriptionToList(keyPressInterpreter.ValidCommands);
            invalidKeyMessage = data.InvalidKeyMessage(keyPressInterpreter.ValidCommands);
            AnnounceRules();
        });
    }

    public override void DidPressKey(string key)
    {
        var sign = keyPressInterpreter.EvaluateInputKey(key);
        if (sign == "")
        {
            badKeyCounter++;
            if (badKeyCounter == 3) { Print(invalidKeyMessage); }
            return;
        }

        badKeyCounter = 0;

        gameMatch.EvaluatePlayerSign(sign);
        EvaluateGameState(gameMatch.OutCome);
    }

    private void EvaluateGameState(OutCome currentGameState)
    {
        switch (currentGameState)
        {
            case playerWin:
                Print(PlayerWinsMatch);
                Print(NextMatchAnnouncement);
                break;
            case computerWin:
                Print(ComputerWinsMatch);
                Print(NextMatchAnnouncement);
                break;
            case tie:
                Print($"Aaand tie... somehow!");
                Print(NextMatchAnnouncement);
                break;
            case inProgress:
                break;
        }
        AnnounceNextRound();
    }

    void AnnounceNextRound()
    {
        Print(NextRoundAnnouncement);
    }

    void AnnounceRules()
    {
        rulesDescriptionToList.ForEach(rule =>
        {
            Print(rule);
        });
    }
}

