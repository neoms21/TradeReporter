using System;
using System.Runtime.CompilerServices;

namespace TradeReporter.Interfaces
{
    public interface ILogger
    {

        /// <summary>Logs the specified app type.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="callerFile">The caller file.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        void LogException(Exception exception, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMemberName = "");

        /// <summary>
        /// Logs the message as trace level
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerFile">The caller file.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        void LogTrace(string message, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMemberName = "");

        /// <summary>
        /// Logs the message as info level
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerFile">The caller file.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        void LogInfo(string message, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMemberName = "");
    }
}
