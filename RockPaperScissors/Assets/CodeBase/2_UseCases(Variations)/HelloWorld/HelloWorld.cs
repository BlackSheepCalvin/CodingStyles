public class HelloWorld: Variation
{
    public HelloWorld(IPrinter printer) : base(printer) {}

    public override void Start()
    {
        Print("Hello World");
    }

    public override void DidPressKey(string key)
    {
        Print("Noted.");
    }
}
