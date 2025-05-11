using System;
using FluentAssertions;
using Jint;
using NUnit.Framework;

namespace LSL.Evaluation.Jint.Tests;

public class JintEvaluatorFactoryTests
{
    [Test]
    public void GivenABuiltEvaluator_ThenItShouldPerformAsExpected()
    {
        // Arrange
        var sut = new JintEvaluatorFactory()
            .Build(c =>
            {
                c.AddCode("var value1 = 12");
                c.SetValue("by2", (int i) => i *2);
                c.Configure(s => s
                    .ConfigureEngine(e => e.Execute("var value2 = 24"))
                    .ConfigureOptions(o => o.AllowClr())
                );
            });

        // Act        
        var result = sut.Evaluate("by2(12) + value1 + value2 + System.Math.Sqrt(144)");
        
        // Assert
        result.Should().BeEquivalentTo(72);
    }
}