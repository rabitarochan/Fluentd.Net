namespace Fluentd.Net
{

    /// <summary>Interface for match-pattern of tag.</summary>
    internal interface IMatchPattern
    {
        bool IsMatch(string tag);
    }

}