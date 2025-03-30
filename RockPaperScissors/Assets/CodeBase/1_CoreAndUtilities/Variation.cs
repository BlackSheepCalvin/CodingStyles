// Hint: interface segregation principle (KeyInputUser is a separate interface, and not part of Variation)
// in hindsight, this is kind of an overkill, because ALL variations, except helloworld will use a keypress... duh!
// so it is a bit of an overengineering. I guess i'll leave it as is as an evidence that interface segregation can be broken sometimes.

public abstract class Variation: PrinterUser
{
    public Variation(IPrinter printer): base (printer) {}

    public abstract void Start();

    public abstract void DidPressKey(string key);
}

public abstract class PrinterUser
{
    private IPrinter printer; //interesting... i restrict this to private, i wonder if its a good idea.

    public PrinterUser(IPrinter printer) // Hint: Dependency injection + Dependency inversion principle
    {
        this.printer = printer;
    }
    protected void Print(string text)
    {
        printer.Print(text);
    }
}
