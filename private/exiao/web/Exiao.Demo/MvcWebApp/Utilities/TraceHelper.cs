// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TraceHelper.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the TraceHelper type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System;
    using System.Diagnostics;
    using System.Text;

    using Exiao.Demo.DataAccess;

    /// <summary>
    /// Defines the TraceHelper type.
    /// </summary>
    internal static class TraceHelper
    {
        #region Methods

        /// <summary>
        /// Traces the error.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void TraceError(string format, params object[] args)
        {
            var traceMessage = GenerateTraceMessage(format, args);

            Trace.TraceError(traceMessage);
            WriteTraceLogToDb("Error", traceMessage);
        }

        /// <summary>
        /// Traces the warning.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void TraceWarning(string format, params object[] args)
        {
            var traceMessage = GenerateTraceMessage(format, args);

            Trace.TraceWarning(traceMessage);
            WriteTraceLogToDb("Warning", traceMessage);
        }

        /// <summary>
        /// Traces the information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void TraceInformation(string format, params object[] args)
        {
            var traceMessage = GenerateTraceMessage(format, args);

            Trace.TraceInformation(traceMessage);
            WriteTraceLogToDb("Information", traceMessage);
        }

        /// <summary>
        /// Generates the trace message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The formatted trace message.</returns>
        private static string GenerateTraceMessage(string format, params object[] args)
        {
            var stringBuilder = new StringBuilder(256);

            stringBuilder.AppendFormat("[{0:o}] ", DateTime.UtcNow);
            stringBuilder.AppendFormat(format, args);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Writes the trace log to database.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        private static void WriteTraceLogToDb(string level, string message)
        {
            if (!ConfigHelper.WriteTraceLogToDb)
            {
                return;
            }

            using (var db = new TraceLogDbContext())
            {
                var creationTime = DateTime.UtcNow;
                db.TraceLogEntries.Add(
                    new TraceLogEntry
                        {
                            Level = level,
                            Message = message,
                            CreationTime = creationTime,
                            CreationTimeStamp = creationTime.Ticks,
                        });
                db.SaveChanges();
            }
        }

        #endregion
    }
}