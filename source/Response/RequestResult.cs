#pragma warning disable CS1591

using System;
using System.Net;

namespace HttpClientBuilder
{

    public enum ResultStatus
    {
        Success,
        HttpStatusError,
        GeneralException
    }
    /// <summary>
    /// The default implementation of the <seealso cref="IRequestResult"/>
    /// </summary>
    public readonly struct RequestResult: IRequestResult
    {
        #region Implementation of IResponseCode

        /// <inheritdoc />
        public ResultStatus Status { get; }

        /// <inheritdoc />
        public HttpStatusCode? StatusCode { get; }

        #endregion

        #region Implementation of IRequestResult

        /// <inheritdoc />
        public bool Success => Error == null;

        /// <inheritdoc />
        public Exception? Error { get; }

        #endregion

        /// <summary>
        /// Creates a new successful result.
        /// </summary>
        /// <param name="code">Http Status Code</param>
        public RequestResult(HttpStatusCode code)
        {
            Status = ResultStatus.Success;
            StatusCode = code;
            Error = null;
        }

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <param name="error">The reason the failure occurred.</param>
        public RequestResult(Exception error)
        {
            Status = ResultStatus.GeneralException;
            StatusCode = null;
            Error = error;
        }

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="error">The reason the failure occurred.</param>
        public RequestResult(HttpStatusCode code, Exception error)
        {
            Status = ResultStatus.HttpStatusError;
            StatusCode = code;
            Error = error;
        }

        public static implicit operator Exception?(RequestResult result) => result.Error;
        public static implicit operator HttpStatusCode?(RequestResult result) => result.StatusCode;

        public static implicit operator RequestResult(Exception exception) => new (exception);
        public static implicit operator RequestResult?(HttpStatusCode code) => new (code);
    }
    
    /// <summary>
    /// The default implementation of the <seealso cref="IRequestResult{TSuccessValue}"/>
    /// </summary>
    /// <typeparam name="TSuccessValue">Type of object stored in the response.</typeparam>
    public readonly struct RequestResult<TSuccessValue> : IRequestResult<TSuccessValue> where TSuccessValue : class
    {
        #region Implementation of IResponseCode

        /// <inheritdoc />
        public ResultStatus Status { get; }

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

        #endregion

        /// <summary>
        /// Creates a new successful result.
        /// </summary>
        /// <param name="code">Http Status Code</param>
        /// <param name="value">Value Stored in the Result.</param>
        public RequestResult(HttpStatusCode code, TSuccessValue value)
        {
            Status = ResultStatus.Success;
            StatusCode = code;
            Value = value;
            Error = null;
        }

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <param name="error">The reason the failure occurred.</param>
        public RequestResult(Exception error)
        {
            Status = ResultStatus.GeneralException;
            StatusCode = null;
            Value = default;
            Error = error;
        }

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="error">The reason the failure occurred.</param>
        public RequestResult(HttpStatusCode code, Exception error)
        {
            Status = ResultStatus.HttpStatusError;
            StatusCode = code;
            Value = default;
            Error = error;
        }

        public static implicit operator bool(RequestResult<TSuccessValue> result) => result.Success;
        public static implicit operator TSuccessValue?(RequestResult<TSuccessValue> result) => result.Value;
        public static implicit operator Exception?(RequestResult<TSuccessValue> result) => result.Error;

        public static implicit operator RequestResult<TSuccessValue>(TSuccessValue value) => new(HttpStatusCode.OK, value);
        public static implicit operator RequestResult<TSuccessValue>(Exception exception) => new(exception);
    }
}
