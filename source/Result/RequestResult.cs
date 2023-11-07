using System;
using System.Net;

namespace HttpClientBuilder
{
    
    public readonly struct RequestResult<TSuccessValue> : IRequestResult<TSuccessValue>
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

        #endregion

        public RequestResult()
        {
            StatusCode = null;
            Value = default;
            Error = null;
        }

        public RequestResult(HttpStatusCode code, TSuccessValue value)
        {
            StatusCode = code;
            Value = value;
            Error = null;
        }
        public RequestResult(Exception error)
        {
            StatusCode = null;
            Value = default;
            Error = error;
        }

        public static implicit operator bool(RequestResult<TSuccessValue> result) => result.Success;
        public static implicit operator TSuccessValue?(RequestResult<TSuccessValue> result) => result.Value;
        public static implicit operator Exception?(RequestResult<TSuccessValue> result) => result.Error;

        public static implicit operator RequestResult<TSuccessValue>(TSuccessValue value) => new(HttpStatusCode.OK, value);
        public static implicit operator RequestResult<TSuccessValue>(Exception exception) => new(exception);

    }
    
    public readonly struct RequestWithErrorResult<TSuccessValue, TErrorValue> : IRequestResultWithError<TSuccessValue, TErrorValue>
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

        public RequestWithErrorResult()
        {
            StatusCode = null;
            Value = default;
            ErrorValue = default;
            Error = null;
        }

        public RequestWithErrorResult(HttpStatusCode code, TSuccessValue value)
        {
            StatusCode = code;
            Value = value;
            ErrorValue = default;
            Error = null;
        }
        
        public RequestWithErrorResult(HttpStatusCode code, TErrorValue errorValue)
        {
            StatusCode = code;
            Value = default;
            ErrorValue = errorValue;
            Error = null;
        }

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
}
