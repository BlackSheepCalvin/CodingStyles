using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class KeyPressInterpreterTests
{
    private KeyPressInterpreter interpreter;

    [SetUp]
    public void Setup()
    {
        // Initialize with valid commands
        interpreter = new KeyPressInterpreter(new List<string> { "rock", "scissors", "spock" });
    }

    [Test]
    public void Test_Recognizes_Immediate_Command()
    {
        Assert.AreEqual("rock", interpreter.EvaluateInputKey("r"));
    }

    [Test]
    public void Test_Waiting_For_Completion()
    {
        Assert.AreEqual("", interpreter.EvaluateInputKey("s"));
        Assert.AreEqual("scissors", interpreter.EvaluateInputKey("c"));
    }

    [Test]
    public void Test_Invalid_Input_Clears_Memory()
    {
        Assert.AreEqual("", interpreter.EvaluateInputKey("s"));
        Assert.AreEqual("", interpreter.EvaluateInputKey("t"));
        Assert.AreEqual("", interpreter.EvaluateInputKey("c"));
    }

    [Test]
    public void Test_Multiple_Valid_Inputs()
    {
        Assert.AreEqual("", interpreter.EvaluateInputKey("s"));
        Assert.AreEqual("scissors", interpreter.EvaluateInputKey("c"));
        Assert.AreEqual("rock", interpreter.EvaluateInputKey("r"));
    }

    [Test]
    public void Test_Ambiguous_Inputs()
    {
        Assert.AreEqual("", interpreter.EvaluateInputKey("s"));
        Assert.AreEqual("spock", interpreter.EvaluateInputKey("p"));

        Assert.AreEqual("", interpreter.EvaluateInputKey("s"));
        Assert.AreEqual("scissors", interpreter.EvaluateInputKey("c"));
    }
}

