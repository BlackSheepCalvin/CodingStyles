using System.Collections.Generic;
using System.Linq;

// Hint: YAGNI / Error handling / overengineer / overthinking / fearless developer / productivity etc
// what about duplicates?
// see comment in the GameData -> the parser's model/DTO
// probably that is the best place to fix duplication. But does it really matter?
// Is it worth the time spending on investigation on best solution for this? Instead of keep developing?
// If users provide the data for you then sure it does.
// But if its you, or backend, you can expect them not to make these silly mistakes.
// Also maybe you DO want to find the error.
// What if its important that you do some kind of error handling when one of those values is wrong?
// ("rock", "paper", "scissors", "spock", "rock") (rock is supposed to be lizard!)
// I'd say you can think about this forever. But is it worth it?
// You should just keep developing, keeping the code as simple and concise as possible
// and when you do have a reason to fix something / think about some real problem
// then you fix / think about it. 
// You may think on big/complex projects its different thuogh, and you can't get away with not thinking about this.
// Well... on big projects i'd say its even worse. - Depending on your managers/product owners!
// You'd have to do meetings, explain to product owners, wait for decisions, wait for designs... LOOOSING TIME!
// Just a reminder though -> we're talking about rock paper scissors...
// how many times do you think someone would want to change this? and how much money would it cost anyone if there is an error here?
// The more important the task is -> the more roboust code you need.
// Talking about overthinking... this comment is quite long now.
public class KeyPressInterpreter
{
    private List<(string, string)> validCommands;
    private string currentInput = "";

    public KeyPressInterpreter(List<string> validCommands)
    {
        this.validCommands = validCommands
            .Select(cmd => (GetMinimumRecognizableString(cmd, validCommands), cmd))
            .OrderByDescending(cmd => cmd.Item2.Length)
            .ToList();
    }

    private string GetMinimumRecognizableString(string command, List<string> allCommands)
    {
        for (int i = 1; i <= command.Length; i++)
        {
            string prefix = command.Substring(0, i);

            if (allCommands.Count(cmd => cmd.StartsWith(prefix)) == 1)
            {
                return prefix;
            }
        }

        return command;
    }

    public string EvaluateInputKey(string key)
    {
        currentInput += key.ToLower();

        foreach (var command in validCommands)
        {
            if (currentInput == command.Item1)
            {
                string recognizedCommand = command.Item2;
                currentInput = "";
                return recognizedCommand;
            }
        }

        if (!validCommands.Any(cmd => cmd.Item1.StartsWith(currentInput)))
        {
            currentInput = "";
            return "";
        }

        return "";
    }
}
