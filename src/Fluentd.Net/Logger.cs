using System;

namespace Fluentd.Net
{
    public class Logger : ILogger
    {
        private readonly NLog.Logger logger = NLog.LogManager.GetLogger("fluentd");

        public void Trace(string message, params object[] args)
        {
            logger.Trace(string.Format(message, args));
        }

        public void TraceException(Exception ex, string message, params object[] args)
        {
            logger.TraceException(string.Format(message, args), ex);
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(string.Format(message, args));
        }

        public void DebugException(Exception ex, string message, params object[] args)
        {
            logger.DebugException(string.Format(message, args), ex);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(string.Format(message, args));
        }

        public void InfoException(Exception ex, string message, params object[] args)
        {
            logger.InfoException(string.Format(message, args), ex);
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(string.Format(message, args));
        }

        public void WarnException(Exception ex, string message, params object[] args)
        {
            logger.WarnException(string.Format(message, args), ex);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(string.Format(message, args));
        }

        public void ErrorException(Exception ex, string message, params object[] args)
        {
            logger.ErrorException(string.Format(message, args), ex);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(string.Format(message, args));
        }

        public void FatalException(Exception ex, string message, params object[] args)
        {
            logger.FatalException(string.Format(message, args), ex);
        }
    }
}
