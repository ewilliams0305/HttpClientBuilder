using System;

namespace HttpClientBuilder;

/// <summary>
/// An exception that will occur when evaluating a <seealso cref="IResponse{TSuccessValue}"/> interface.
/// <seealso cref="ResponseExtensions"/>
/// </summary>
public sealed class RequestResultException: Exception  
{
    /// <summary>
    /// Creates a new exception with the message provided.
    /// </summary>
    /// <param name="message">Message explaining when the result was converted to a failure.</param>
    public RequestResultException(string message)
        :base(message)
    {
            
    }
}