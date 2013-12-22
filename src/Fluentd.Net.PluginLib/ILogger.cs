using System;

namespace Fluentd.Net
{
    public interface ILogger
    {
        void Trace(string message, params object[] args);

        void TraceException(Exception ex, string message, params object[] args);

        void Debug(string message, params object[] args);

        void DebugException(Exception ex, string message, params object[] args);

        void Info(string message, params object[] args);

        void InfoException(Exception ex, string message, params object[] args);

        void Warn(string message, params object[] args);

        void WarnException(Exception ex, string message, params object[] args);

        void Error(string message, params object[] args);

        void ErrorException(Exception ex, string message, params object[] args);

        void Fatal(string message, params object[] args);

        void FatalException(Exception ex, string message, params object[] args);
    }
}
