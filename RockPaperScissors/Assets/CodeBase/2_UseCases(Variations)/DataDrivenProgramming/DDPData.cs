using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public struct GameData
{
    public string rulesDescription;
    public List<string> signs; // you could use HashSet instead of List - see comments at KeyPressInterpreter
    public List<Rule> rules;
    public string invalidKeyMessage;
    public int matchLength;

    public List<string> RulesDescriptionToList(List<(string, string)> commandList) // Hint: indicate that something is costly by making it a method, and not a computed property
    {
        var capitalizedSigns = signs.ToCapitalizedFirstList();
        var gameTitleText = string.Join(", ", capitalizedSigns);

        var rulesCount = rules.Count;
        var rulesText = string.Join(",  ", rules.Select((item, index) => { 
            return index < rulesCount - 1 ? $"{item.ToString().CapitalizeFirst()}" : $"And {item}"; 
        }).ToList());

        string formattedDescription = string.Format(
            rulesDescription,
            gameTitleText,
            CommandExplanationText(commandList),
            rulesText,
            matchLength
        );

        return formattedDescription.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    private string CommandExplanationText(List<(string, string)> commandList)
    {
        var commandCount = commandList.Count;
        return string.Join(", ", commandList.Select((x, index) => {
            return index < commandCount - 1 ? $"'{x.Item1}' for {x.Item2}" : $"and '{x.Item1}' for {x.Item2}"; 
        }).ToList());
    }

    public string InvalidKeyMessage(List<(string, string)> commandList)
    { // Hint: Instead of these functions, shouldn't i just make an other data data, used by the program, and maybe create a class that transforms this data?
      // Wouldn't that be more scalable? Wouldn't this look bad over time? How would someone know to call InvalidKeyMessage(commandList) instead of invalidKeyMessage? Also this is costly? Or can be costly in future?
      // I mean these are valid points, but 1: is it good now? 2: how many places do you have to change something when you make a change? 3: what if the majority of the data is simple, that requires no transformation? Then you just copy paste copy paste duplicate, and confuse
      // 4: isnt it nice that you can access the same data throughout the whole app that is the source of truth?
      // If you keep making transformations of the data whenever you feel like it, because of encapsulation, then how would you know
      //    a: where the data comes from? b: who uses it? c: why is it the way it is, what transformations were made?
      //    if you keep using the same data everywhere, then you'd know the answer to all 3. a: the source, b: EVERYONE, c: Because this is the way. No transformations except the helper func you just called.
      // BTW these helpers could be in extensions too, in a different file, if you prefer.
        return string.Format(invalidKeyMessage, CommandExplanationText(commandList));
    }
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
