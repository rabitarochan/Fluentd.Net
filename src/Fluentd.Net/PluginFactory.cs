using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Fluentd.Net
{
    using InputPluginFactory = ExportFactory<IInputPlugin, IPluginMetadata>;
    using OutputPluginFactory = ExportFactory<IOutputPlugin, IPluginMetadata>;

    /// <summary>Factory of plugin instance.</summary>
    [Export]
    public class PluginFactory
    {
        private readonly IDictionary<string, InputPluginFactory> inputPluginFactoryCache;
        private readonly IDictionary<string, OutputPluginFactory> outputPluginFactoryCache;

        [Obsolete("Do not use this constructor when creating the instance, using MEF.")]
        public PluginFactory()
        {
            inputPluginFactoryCache = new Dictionary<string, InputPluginFactory>();
            outputPluginFactoryCache = new Dictionary<string, OutputPluginFactory>();
        }
            
        [ImportMany]
        public IEnumerable<InputPluginFactory> InputPluginFactories { get; set; }

        [ImportMany]
        public IEnumerable<OutputPluginFactory> OutputPluginFactories { get; set; }

        /// <summary>Create new input plugin instance.</summary>
        /// <param name="name">Input plugin name.</param>
        /// <returns>The new input plugin instance.</returns>
        public IInputPlugin CreateInputPlugin(string name)
        {
            InputPluginFactory factory;
            if (!inputPluginFactoryCache.TryGetValue(name, out factory)) {
                factory = InputPluginFactories.SingleOrDefault(x => x.Metadata.Name == name);
                if (factory == null) return null;

                inputPluginFactoryCache.Add(name, factory);
            }

            return factory.CreateExport().Value;
        }

        /// <summary>Create new output plugin instance.</summary>
        /// <param name="name">Output plugin name.</param>
        /// <returns>The new output plugin instance.</returns>
        public IOutputPlugin CreateOutputPlugin(string name)
        {
            OutputPluginFactory factory;
            if (!outputPluginFactoryCache.TryGetValue(name, out factory)) {
                factory = OutputPluginFactories.SingleOrDefault(x => x.Metadata.Name == name);
                if (factory == null) return null;

                outputPluginFactoryCache.Add(name, factory);
            }

            return factory.CreateExport().Value;
        }
    }
}
