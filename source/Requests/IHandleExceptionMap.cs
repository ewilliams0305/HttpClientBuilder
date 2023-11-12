using System;
using System.Net;

namespace HttpClientBuilder;

/// <summary>
/// Maps an exception to a type.
/// </summary>
public interface IHandleExceptionMap
{
    /// <summary>
    /// Maps an exception to a type
    /// </summary>
    /// <typeparam name="TExceptionType"></typeparam>
    /// <param name="responseTypeMap"></param>
    /// <returns></returns>
    IRequest HandleException<TExceptionType>(Func<HttpStatusCode, TExceptionType> responseTypeMap) where TExceptionType : Exception;
}