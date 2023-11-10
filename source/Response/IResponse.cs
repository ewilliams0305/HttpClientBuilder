using System;
using System.Net;

namespace HttpClientBuilder
{
    /// <summary>
    /// Provides the <seealso cref="HttpStatusCode"/> as a response from an <seealso cref="IHttpClient"/> request.
    /// </summary>
    public interface IResponseCode
    {
        /// <summary>
        /// returns the <seealso cref="HttpStatusCode"/> as a response from an <seealso cref="IHttpClient"/> request.
        /// </summary>
        HttpStatusCode? StatusCode { get; }
    }

    /// <summary>
    /// Provides a success or fail returned from an <seealso cref="IHttpClient"/> request.
    /// The request will be successful when the exception is null.  
    /// </summary>
    public interface IResponse : IResponseCode
    {
        /// <summary>
        /// The current state of the result object.
        /// Results can only ever be an exception, http status error, or successful.
        /// </summary>
        ResponseState Status { get; }

        /// <summary>
        /// True while the <seealso cref="IResponse{TSuccessValue}"/> contains no exception data.
        /// </summary>
        bool Success { get; }
        /// <summary>
        /// Stores the response as an exception should one occur.
        /// </summary>
        Exception? Error { get; }
    }

    /// <summary>
    /// Stores a value from a request.
    /// </summary>
    /// <typeparam name="TSuccessValue">Value returned from the request</typeparam>
    public interface IResponse<out TSuccessValue> : IResponse where TSuccessValue : class
    {
        /// <summary>
        /// Stored value.  This value will be null if there was an exception.  This value will never be null if the <seealso cref="IResponse"/> is successful.
        /// </summary>
        TSuccessValue? Value { get; }
    }

    /// <summary>
    /// Stores two values from a request.
    /// When the response was successful the data will contain the TSuccessValue
    /// When the response was 400 or greater the value will be stored as a TErrorValue.
    /// </summary>
    /// <typeparam name="TSuccessValue">Type of happy path object</typeparam>
    /// <typeparam name="TErrorValue">Type of failure path object</typeparam>
    public interface IResponseWithError<out TSuccessValue, out TErrorValue> : IResponse<TSuccessValue> 
        where TSuccessValue : class 
        where TErrorValue : class
    {
        /// <summary>
        /// The value converted from the failed response.
        /// </summary>
        TErrorValue? ErrorValue { get; }
    }
}