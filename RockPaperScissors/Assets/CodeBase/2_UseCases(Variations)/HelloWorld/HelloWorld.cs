public class HelloWorld: Variation
{
    public HelloWorld(IPrinter printer) : base(printer) {}

    public override void Start()
    {
        printer.Print("Hello World");
    }
}
