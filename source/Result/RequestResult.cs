using System;

namespace HttpClientBuilder.Result
{
    
    public readonly struct RequestResult<TSuccessValue> : IRequestResult<TSuccessValue>
    {
        public HttpStatusCode Status ode 

        #region Implementation of IRequestResult<out TSuccessValue>

        /// <inheritdoc />
        public bool Success => Value != null;

        /// <inheritdoc />
        public TSuccessValue? Value { get; }

        /// <inheritdoc />
        public Exception? Error { get; }

        #endregion

        public RequestResult(TSuccessValue value)
        {
            Value = value;
            Error = null;
        }
        public RequestResult(Exception error)
        {
            Value = default(TSuccessValue);
            Error = error;
        }

        public static implicit operator bool(RequestResult<TSuccessValue> result) => result.Success;
        public static implicit operator TSuccessValue?(RequestResult<TSuccessValue> result) => result.Value;
        public static implicit operator Exception?(RequestResult<TSuccessValue> result) => result.Error;

        public static implicit operator RequestResult<TSuccessValue>(TSuccessValue value) => new(value);
        public static implicit operator RequestResult<TSuccessValue>(Exception exception) => new(exception);
    }
    
    public readonly struct RequestWithErrorResult<TSuccessValue, TErrorValue> : IRequestResultWithError<TSuccessValue, TErrorValue>
    {
        #region Implementation of IRequestResult<out TSuccessValue>

        /// <inheritdoc />
        public bool Success => Value != null;

        /// <inheritdoc />
        public TSuccessValue? Value { get; }

        /// <inheritdoc />
        public Exception? Error { get; }

        #region Implementation of IRequestResultWithError<out TSuccessValue,out TErrorValue>

        /// <inheritdoc />
        public TErrorValue? ErrorValue { get; }

        #endregion

        #endregion

        public RequestWithErrorResult(TSuccessValue value)
        {
            Value = value;
            ErrorValue = default(TErrorValue);
            Error = null;
        }

        public RequestWithErrorResult(Exception error)
        {
            Value = default(TSuccessValue);
            ErrorValue = default(TErrorValue);
            Error = error;
        }
        
        public RequestWithErrorResult(TErrorValue errorValue)
        {
            Value = default(TSuccessValue);
            ErrorValue = errorValue;
            Error = null;
        }

        public static implicit operator bool(RequestWithErrorResult<TSuccessValue, TErrorValue> result) => result.Success;
        public static implicit operator TSuccessValue?(RequestWithErrorResult<TSuccessValue, TErrorValue> result) => result.Value;
        public static implicit operator TErrorValue?(RequestWithErrorResult<TSuccessValue, TErrorValue> result) => result.ErrorValue;
        public static implicit operator Exception?(RequestWithErrorResult<TSuccessValue, TErrorValue> result) => result.Error;

        public static implicit operator RequestWithErrorResult<TSuccessValue, TErrorValue>(TSuccessValue value) => new(value);
        public static implicit operator RequestWithErrorResult<TSuccessValue, TErrorValue>(TErrorValue value) => new(value);
        public static implicit operator RequestWithErrorResult<TSuccessValue, TErrorValue>(Exception exception) => new(exception);
    }
}
