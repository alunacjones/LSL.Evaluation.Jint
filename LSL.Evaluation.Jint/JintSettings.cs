using System;
using System.Collections.Generic;
using Jint;

namespace LSL.Evaluation.Jint;

/// <summary>
/// Settings for the Jint Javascript engine
/// </summary>
public class JintSettings
{
    internal List<Action<Options>> OptionsConfigurators = new();
    internal List<Action<Engine>> EngineConfigurators = new();

    /// <summary>
    /// Add a configurator to setup a Jint Options instance
    /// </summary>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public JintSettings ConfigureOptions(Action<Options> configurator)
    {
        OptionsConfigurators.Add(configurator);
        return this;
    }

    /// <summary>
    /// Add a configurator to setup a Jint Engine instance
    /// </summary>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public JintSettings ConfigureEngine(Action<Engine> configurator)
    {
        EngineConfigurators.Add(configurator);
        return this;
    }
}