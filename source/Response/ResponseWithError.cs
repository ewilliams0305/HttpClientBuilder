using System;
using System.Net;
using System.Net.Http.Headers;

namespace HttpClientBuilder;

#pragma warning disable CS1591

/// <summary>
/// The default implementation of the <seealso cref="IResponse{TSuccessValue,TErrorValue}"/>
/// </summary>
/// <typeparam name="TSuccessValue">Type of object stored in the response.</typeparam>
/// <typeparam name="TErrorValue">Type of object stored when the request returned a 400 or greater.</typeparam>
public readonly struct ResponseWithError<TSuccessValue, TErrorValue> : IResponse<TSuccessValue, TErrorValue> where TSuccessValue : class where TErrorValue : class
{
    #region Implementation of IResponseCode

    /// <inheritdoc />
    public HttpResponseHeaders? Headers { get; }

    /// <inheritdoc />
    public ResponseState Status { get; }

    /// <inheritdoc />
    public HttpStatusCode? StatusCode { get; }

    #endregion

    #region Implementation of IRequestResult<out TSuccessValue>

    /// <inheritdoc />
    public bool Success => Value != null && Error == null;

    /// <inheritdoc />
    public TSuccessValue? Value { get; }

    /// <inheritdoc />
    public Exception? Error { get; }

    #region Implementation of IRequestResultWithError<out TSuccessValue,out TErrorValue>

    /// <inheritdoc />
    public TErrorValue? ErrorValue { get; }

    #endregion

    #endregion

    /// <summary>
    /// Creates a new successful result.
    /// </summary>
    /// <param name="code">Http Status Code</param>
    /// <param name="headers"></param>
    /// <param name="value">Value Stored in the Result.</param>
    public ResponseWithError(HttpStatusCode code, HttpResponseHeaders headers,  TSuccessValue value)
    {
        Status = ResponseState.Success;
        StatusCode = code;
        Headers = headers;
        Value = value;
        ErrorValue = default;
        Error = null;
    }

    /// <summary>
    /// Creates a new error result.
    /// </summary>
    /// <param name="code">Http Status Code</param>
    /// <param name="headers"></param>
    /// <param name="errorValue">Value Stored in the Result.</param>
    public ResponseWithError(HttpStatusCode code, HttpResponseHeaders headers, TErrorValue errorValue)
    {
        Status = ResponseState.HttpStatusError;
        StatusCode = code;
        Headers = headers;
        Value = default;
        ErrorValue = errorValue;
        Error = null;
    }

    /// <summary>
    /// Creates a new failed result.
    /// </summary>
    /// <param name="error">The reason the failure occurred.</param>
    public ResponseWithError(Exception error)
    {
        Status = ResponseState.Exception;
        StatusCode = null;
        Headers = null;
        Value = default;
        ErrorValue = default;
        Error = error;
    }
        
    public static implicit operator bool(ResponseWithError<TSuccessValue, TErrorValue> result) => result.Success;
    public static implicit operator TSuccessValue?(ResponseWithError<TSuccessValue, TErrorValue> result) => result.Value;
    public static implicit operator TErrorValue?(ResponseWithError<TSuccessValue, TErrorValue> result) => result.ErrorValue;
    public static implicit operator Exception?(ResponseWithError<TSuccessValue, TErrorValue> result) => result.Error;
    public static implicit operator ResponseWithError<TSuccessValue, TErrorValue>(Exception exception) => new(exception);
}