namespace Fluentd.Net
{
    public interface IFluentdContext
    {
        IEngine Engine { get; }
        ILogger Logger { get; }
    }
}