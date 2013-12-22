namespace Fluentd.Net
{
    /// <summary>Results finished process.</summary>
    public enum FinishStatus
    {
        /// <summary>The process Finished correctly by the user operation.</summary>
        Stop,

        /// <summary>The process was aborted, but the retry is possible.</summary>
        CanRetry,

        /// <summary>The process was aborted.</summary>
        Abort
    }
}