using System;
using System.Diagnostics;
using log4net;

namespace Topcourse.Utility
{
    public class Log4Net
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ILog _lDebug = LogManager.GetLogger("DEBUG");
        private static readonly ILog _lWarn = LogManager.GetLogger("WARN");
        private static readonly ILog _lInfo = LogManager.GetLogger("INFO");
        private static readonly ILog _lEror = LogManager.GetLogger("ERROR");

        public static void PublishException(Exception ex)
        {
            _lDebug.Debug($"{GetCalleeString()}>>Exception: ", ex);
        }

        public static void LogError(string message)
        {
            _lEror.Error($"{GetCalleeString()}>>LogError: {message}");
        }
        public static void LogInfo(string message, int line = 0)
        {
            _lInfo.Info($"{GetCalleeString()}| Line:{line}>>LogInfo: {message}");
        }
        public static void LogWarning(string message)
        {
            _lEror.Error($"{GetCalleeString()}>>: {message}");
        }
        private static string GetCalleeString()
        {
            foreach (StackFrame sf in new StackTrace().GetFrames())
            {
                if (!string.IsNullOrEmpty(sf.GetMethod().ReflectedType.Namespace) && !typeof(Log4Net).FullName.StartsWith(sf.GetMethod().ReflectedType.Namespace))
                {
                    return string.Format("{0}.{1} ", sf.GetMethod().ReflectedType.Name, sf.GetMethod().Name, sf.GetFileLineNumber());
                }
            }
            return string.Empty;
        }
    }
}
