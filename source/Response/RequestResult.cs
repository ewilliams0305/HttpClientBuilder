#pragma warning disable CS1591

using System;
using System.Net;
using System.Net.Http.Headers;

namespace HttpClientBuilder
{
    /// <summary>
    /// The default implementation of the <seealso cref="IResponse"/>
    /// </summary>
    public readonly struct Response: IResponse
    {
        #region Implementation of IResponseCode

        /// <inheritdoc />
        public HttpResponseHeaders? Headers { get; }

        /// <inheritdoc />
        public ResponseState Status { get; }

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
        /// <param name="headers">Http response headers returned from the server</param>
        public Response(HttpStatusCode code, HttpResponseHeaders? headers)
        {
            Status = ResponseState.Success;
            StatusCode = code;
            Headers = headers;
            Error = null;
        }

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <param name="error">The reason the failure occurred.</param>
        public Response(Exception error)
        {
            Status = ResponseState.Exception;
            StatusCode = null;
            Headers = null;
            Error = error;
        }

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="error">The reason the failure occurred.</param>
        public Response(HttpStatusCode code, Exception error)
        {
            Status = ResponseState.HttpStatusError;
            StatusCode = code;
            Headers = null;
            Error = error;
        }

        public static implicit operator Exception?(Response result) => result.Error;
        public static implicit operator HttpStatusCode?(Response result) => result.StatusCode;

        public static implicit operator Response(Exception exception) => new (exception);

    }
    
    /// <summary>
    /// The default implementation of the <seealso cref="IResponse{TSuccessValue}"/>
    /// </summary>
    /// <typeparam name="TSuccessValue">Type of object stored in the response.</typeparam>
    public readonly struct RequestResult<TSuccessValue> : IResponse<TSuccessValue> where TSuccessValue : class
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

        #endregion

        /// <summary>
        /// Creates a new successful result.
        /// </summary>
        /// <param name="code">Http Status Code</param>
        /// <param name="headers">Http Response headers</param>
        /// <param name="value">Value Stored in the Result.</param>
        public RequestResult(HttpStatusCode code, HttpResponseHeaders? headers, TSuccessValue value)
        {
            Status = ResponseState.Success;
            StatusCode = code;
            Headers = headers;
            Value = value;
            Error = null;
        }

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="headers"></param>
        /// <param name="error">The reason the failure occurred.</param>
        public RequestResult(HttpStatusCode code, HttpResponseHeaders? headers, Exception error)
        {
            Status = ResponseState.HttpStatusError;
            StatusCode = code;
            Headers = headers;
            Value = default;
            Error = error;
        }

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <param name="error">The reason the failure occurred.</param>
        public RequestResult(Exception error)
        {
            Status = ResponseState.Exception;
            Headers = null;
            StatusCode = null;
            Value = default;
            Error = error;
        }

        

        public static implicit operator bool(RequestResult<TSuccessValue> result) => result.Success;
        public static implicit operator TSuccessValue?(RequestResult<TSuccessValue> result) => result.Value;
        public static implicit operator Exception?(RequestResult<TSuccessValue> result) => result.Error;
        public static implicit operator RequestResult<TSuccessValue>(Exception exception) => new(exception);
    }
}
