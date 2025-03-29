// Hint: interface segregation principle (KeyInputUser is a separate interface, and not part of Variation)
// in hindsight, this is kind of an overkill, because ALL variations, except helloworld will use a keypress... duh!
// so it is a bit of an overengineering. I guess i'll leave it as is as an evidence that interface segregation can be broken sometimes.

public abstract class Variation
{
    protected IPrinter printer;

    public Variation(IPrinter printer) // Hint: Dependency injection + Dependency inversion principle
    {
        this.printer = printer;
    }

    public abstract void Start();

    public abstract void DidPressKey(string key);
}