using System;
using System.Collections.Generic;
using System.Linq;
using Jint;
using LSL.Evaluation.Core;

namespace LSL.Evaluation.Jint;

/// <summary>
/// The factory to create a Jint based evaluator
/// </summary>
public class JintEvaluatorFactory : IEvaluatorFactoryWithSettings<JintSettings>
{
    /// <inheritdoc/>
    public IEvaluator Build(Action<IEvaluatorFactoryConfigurationWithSettings<JintSettings>> configurator)
    {
        static void Configure<T>(IEnumerable<Action<T>> configurators, T toConfigure) => 
            configurators.ForEach(i => i.Invoke(toConfigure));

        var config = new EvaluatorFactoryConfigurationWithSettings<JintSettings>();
        var jintSettings = new JintSettings();
        var options = new Options();

        configurator.Invoke(config);

        Configure(config.SettingsConfigurators, jintSettings);
        Configure(jintSettings.OptionsConfigurators, options);

        var engine = new Engine(options);

        Configure(jintSettings.EngineConfigurators, engine);
        Configure(config.CodeToAdd.Select(c => (Action<Engine>)(e => e.Execute(c))), engine);

        return new JintEvaluator(engine);
    }

    internal class JintEvaluator(Engine engine) : IEvaluator
    {
        public object Evaluate(string expression) => engine.Evaluate(expression).ToObject();
    }
}
