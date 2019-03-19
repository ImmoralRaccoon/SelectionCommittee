namespace SelectionCommittee.Logger
{
    public interface ILoggerManager
    {
        /// <summary>
        /// Designates informational messages.
        /// </summary>
        /// <param name="message">String message</param>
        void LogInfo(string message);

        /// <summary>
        /// Designates potentially harmful situations.
        /// </summary>
        /// <param name="message">String message</param>
        void LogWarn(string message);

        /// <summary>
        /// Designates fine-grained informational events.
        /// </summary>
        /// <param name="message">String message</param>
        void LogDebug(string message);

        /// <summary>
        /// Designates error events.
        /// </summary>
        /// <param name="message">String message</param>
        void LogError(string message);
    }
}