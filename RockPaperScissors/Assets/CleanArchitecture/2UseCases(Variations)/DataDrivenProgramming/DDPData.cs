using System.Collections.Generic;

[System.Serializable]
public struct GameData
{
    public string rulesDescription;
    public List<string> signs; // you could use HashSet instead of List - see comments at KeyPressInterpreter
    public List<Rule> rules;
    public int matchLength;
}

[System.Serializable]
public struct Rule
{
    public string winner;
    public string action;
    public string loser;

    public override string ToString()
    {
        return $"{winner} {action} {loser}";
    }
}

