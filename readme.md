[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-evaluation-jint.svg)](https://ci.appveyor.com/project/alunacjones/lsl-evaluation-jint)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.Evaluation.Jint)](https://coveralls.io/github/alunacjones/LSL.Evaluation.Jint)
[![NuGet](https://img.shields.io/nuget/v/LSL.Evaluation.Jint.svg)](https://www.nuget.org/packages/LSL.Evaluation.Jint/)

# LSL.Evaluation.Jint

Provides an evaluator using the [Jint](https://www.nuget.org/packages/Jint/) JavaScript engine

## Quick Start

The following code provides a simple example of evaluating an expression using the `JintEvaluatorFactory`

```csharp
using LSL.Evaluation.Jint;
...
var sut = new JintEvaluatorFactory()
    .Build(c =>
    {
        // Ensures we have a variable called value1 available for
        // evaluation
        // We can add functions and other code such as classes here too
        c.AddCode("var value1 = 12");

        // This sets up Jint specific options and engine settings
        c.Configure(s => s
            // Adds another variable via the jint engine
            .ConfigureEngine(e => e.Execute("var value2 = 24"))
            // Sets up options for the Jint engine
            .ConfigureOptions(o => o.AllowClr())
        );
    });

// System.Math.Sqrt is available due to the options being setup to
// allow CLR types
var result = sut.Evaluate("12 + value1 + value2 + System.Math.Sqrt(144)");
// Result will have a value of 60 

```