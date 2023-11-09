using System;
using System.Net;

namespace HttpClientBuilder;

#pragma warning disable CS1591

/// <summary>
/// The default implementation of the <seealso cref="IRequestResultWithError{TSuccessValue, TErrorValue}"/>
/// </summary>
/// <typeparam name="TSuccessValue">Type of object stored in the response.</typeparam>
/// <typeparam name="TErrorValue">Type of object stored when the request returned a 400 or greater.</typeparam>
public readonly struct RequestWithErrorResult<TSuccessValue, TErrorValue> : IRequestResultWithError<TSuccessValue, TErrorValue> where TSuccessValue : class where TErrorValue : class
{
    #region Implementation of IResponseCode

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
    /// <param name="value">Value Stored in the Result.</param>
    public RequestWithErrorResult(HttpStatusCode code, TSuccessValue value)
    {
        StatusCode = code;
        Value = value;
        ErrorValue = default;
        Error = null;
    }

    /// <summary>
    /// Creates a new error result.
    /// </summary>
    /// <param name="code">Http Status Code</param>
    /// <param name="errorValue">Value Stored in the Result.</param>
    public RequestWithErrorResult(HttpStatusCode code, TErrorValue errorValue)
    {
        StatusCode = code;
        Value = default;
        ErrorValue = errorValue;
        Error = null;
    }

    /// <summary>
    /// Creates a new failed result.
    /// </summary>
    /// <param name="error">The reason the failure occurred.</param>
    public RequestWithErrorResult(Exception error)
    {
        StatusCode = null;
        Value = default;
        ErrorValue = default;
        Error = error;
    }
        
    public static implicit operator bool(RequestWithErrorResult<TSuccessValue, TErrorValue> result) => result.Success;
    public static implicit operator TSuccessValue?(RequestWithErrorResult<TSuccessValue, TErrorValue> result) => result.Value;
    public static implicit operator TErrorValue?(RequestWithErrorResult<TSuccessValue, TErrorValue> result) => result.ErrorValue;
    public static implicit operator Exception?(RequestWithErrorResult<TSuccessValue, TErrorValue> result) => result.Error;

    public static implicit operator RequestWithErrorResult<TSuccessValue, TErrorValue>(TSuccessValue value) => new(HttpStatusCode.OK, value);
    public static implicit operator RequestWithErrorResult<TSuccessValue, TErrorValue>(TErrorValue value) => new(HttpStatusCode.BadRequest, value);
    public static implicit operator RequestWithErrorResult<TSuccessValue, TErrorValue>(Exception exception) => new(exception);
}