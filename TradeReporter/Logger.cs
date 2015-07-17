using System;
using NLog;
using TradeReporter.Interfaces;

namespace TradeReporter
{
    public class Logger : ILogger
    {
        public void LogException(Exception exception, string callerFile = "", string callerMemberName = "")
        {

            LogManager.GetLogger(callerFile).Error(exception.ToString);
        }

        public void LogTrace(string message, string callerFile = "", string callerMemberName = "")
        {

        }

        public void LogInfo(string message, string callerFile = "", string callerMemberName = "")
        {
            LogManager.GetLogger(callerFile).Info(message, callerFile, callerMemberName);
        }
    }
}