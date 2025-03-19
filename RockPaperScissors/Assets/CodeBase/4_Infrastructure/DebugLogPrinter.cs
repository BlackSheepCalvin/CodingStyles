using UnityEngine;

// Originally created for Simple Variation, but in Core assembly (not in frameworks and drivers).
public class DebugLogPrinter : IPrinter
{
    public void Print(string message)
    {
        Debug.Log(message);
    }
}
