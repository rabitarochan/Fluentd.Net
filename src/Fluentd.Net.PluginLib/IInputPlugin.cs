using Fluentd.Net.Config;

namespace Fluentd.Net
{
    /// <summary>Interface for input plugin.</summary>
    public interface IInputPlugin
    {
        /// <summary>Configure plugin.</summary>
        /// <param name="context">Context of Fluent.Net.</param>
        /// <param name="config">Configure element for plugin.</param>
        void Configure(IFluentdContext context, IElement config);

        /// <summary>Starting plugin.</summary>
        void Start();

        /// <summary>Shuttingdown plugin.</summary>
        void Shutdown();
    }

}