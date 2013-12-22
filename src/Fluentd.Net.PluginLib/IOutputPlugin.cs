using Fluentd.Net.Config;

namespace Fluentd.Net
{
    /// <summary>
    /// Interface for output plugin.
    /// </summary>
    public interface IOutputPlugin
    {
        /// <summary>Configure plugin.</summary>
        /// <param name="context">Context of Fluent.Net.</param>
        /// <param name="config">Configure element for plugin.</param>
        void Configure(IFluentdContext context, IElement config);

        /// <summary>Starting plugin.</summary>
        void Start();

        /// <summary>Shuttingdown plugin.</summary>
        void Shutdown();

        /// <summary>Emitting messages.</summary>
        /// <param name="tag">tag</param>
        /// <param name="es">messages</param>
        void Emit(string tag, IEventStream es);
    }

}