using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace TCRB.WEB.Helpers
{
    public class LoggerHelper<T> : ILoggerHelper<T>
    {
        private readonly ILogger<T> _logger;
        public LoggerHelper(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Debug(string message)
        {
            _logger.LogDebug($"{message}");
        }
        public void BeginInfo(string funcName, object data)
        {
            _logger.LogInformation($"Call {funcName} Parameters => {JsonConvert.SerializeObject(data)}");
        }

        public void EndInfo(string funcName, object data)
        {
            _logger.LogInformation($"End {funcName} Result => {JsonConvert.SerializeObject(data)}");
        }

        public void Trace(string message)
        {
            _logger.LogTrace($"{message}");
        }

        public void BeginTrace(string funcName)
        {
            _logger.LogTrace($"Call {funcName}");
        }

        public void EndTrace(string funcName)
        {
            _logger.LogTrace($"End {funcName}");
        }

        public void Information(string message)
        {
            _logger.LogInformation($"{message}");
        }
        public void Information(string funcName, string message)
        {
            _logger.LogInformation($"Function:{funcName} => {message}");
        }
        public void Warning(string message)
        {
            _logger.LogWarning($"{message}");
        }
        public void Warning(string funcName, string message)
        {
            _logger.LogWarning($"Function:{funcName} => {message}");
        }
        public void Error(string message)
        {
            _logger.LogError($"{message}");
        }
        public void Error(string funcName, string message)
        {
            _logger.LogError($"Function:{funcName} => {message}");
        }
        public void ErrorException(Exception ex)
        {
            _logger.LogError(ex, "");
        }
        public void ErrorException(Exception ex, string message)
        {
            _logger.LogError(ex, $"{message}");
        }
        public void Critical(string message)
        {
            _logger.LogCritical($"{message}");
        }
        public void Critical(string funcName, string message)
        {
            _logger.LogCritical($"Function:{funcName} => {message}");
        }
    }
}

