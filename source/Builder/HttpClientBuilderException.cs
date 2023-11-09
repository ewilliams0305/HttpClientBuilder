using System;
using System.Runtime.CompilerServices;

namespace HttpClientBuilder
{
    /// <summary>
    /// A General exception that can throw during the build of a client.  This exception should include enough information to assist and help determine why the builder failed to return a new client.
    /// </summary>
    public sealed class HttpClientBuilderException : Exception
    {
        /// <summary>
        /// Name of the function that failed.
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// Name of the called invoking the function that failed.
        /// </summary>
        public string CallerName { get; set; }

        /// <summary>
        /// Creates a new builder exception with the caller member information.
        /// </summary>
        /// <param name="methodName">Method that threw the exception</param>
        /// <param name="callerName">Caller that invoked the method.</param>
        public HttpClientBuilderException(string methodName, [CallerMemberName] string? callerName = default)
            : base($"Builder failed to {methodName} invoked by {callerName}")
        {
            MethodName = methodName;
            CallerName = callerName?? nameof(HttpClientBuilderException);
        }
    }
}