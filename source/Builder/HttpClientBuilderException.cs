using System;
using System.Runtime.CompilerServices;

namespace HttpClientBuilder
{
    public sealed class HttpClientBuilderException : Exception
    {
        public string MethodName { get; set; }
        public string CallerName { get; set; }

        public HttpClientBuilderException(string methodName, [CallerMemberName] string? callerName = default)
            : base($"Builder failed to {methodName} invoked by {callerName}")
        {
            MethodName = methodName;
            CallerName = callerName?? nameof(HttpClientBuilderException);
        }
    }
}