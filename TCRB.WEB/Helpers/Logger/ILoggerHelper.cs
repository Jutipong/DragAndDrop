using System;

namespace TCRB.WEB.Helpers
{
    public interface ILoggerHelper<T>
    {
        void Trace(string message);
        void BeginTrace(string funcName);
        void EndTrace(string funcName);
        void Debug(string message);
        void BeginInfo(string funcName, object data);
        void EndInfo(string funcName, object data);
        void Information(string message);
        void Information(string funcName, string message);
        void Warning(string message);
        void Warning(string funcName, string message);
        void Error(string message);
        void Error(string funcName, string message);
        void ErrorException(Exception ex);
        void ErrorException(Exception ex, string message);
        void Critical(string message);
        void Critical(string funcName, string message);
    }
}
