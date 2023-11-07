using System;
using System.Net;

namespace HttpClientBuilder
{
    public interface IResponseCode
    {
        HttpStatusCode? StatusCode { get; }
    }
    public interface IRequestResult<out TSuccessValue> : IResponseCode
    {

        bool Success { get; }

        TSuccessValue? Value { get; }
        
        Exception? Error { get; }
    }

    public interface IRequestResultWithError<out TSuccessValue, out TErrorValue> : IRequestResult<TSuccessValue>
    {
        TErrorValue? ErrorValue { get; }
    }
}