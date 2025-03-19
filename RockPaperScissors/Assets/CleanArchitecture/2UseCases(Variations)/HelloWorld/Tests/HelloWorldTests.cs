using System;
using System.Linq;
using NUnit.Framework;

public class HelloWorldTests
{
    [Test]
    public void HelloWorldStart()
    {
        // Hint: Behavior-Driven Development (BDD)
        // Given
        var mockPrinter = new MockPrinter();
        var sut = new HelloWorld(mockPrinter);
        Assert.AreEqual(null, mockPrinter.printCallHistory.FirstOrDefault());

        // When
        sut.Start();
        
        // Then
        Assert.AreEqual("Hello World", mockPrinter.printCallHistory.First());
    }
}
